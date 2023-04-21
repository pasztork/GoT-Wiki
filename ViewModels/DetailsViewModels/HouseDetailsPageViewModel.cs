using GoT_Wiki.Models;
using GoT_Wiki.Services;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;

namespace GoT_Wiki.ViewModels
{
    public class HouseDetailsPageViewModel : DetailsViewModelBase<House>, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public Character CurrentLord { get; set; } = null;
        public Character Heir { get; set; } = null;
        public Character Founder { get; set; } = null;
        public ObservableCollection<Character> SwornMembers { get; } = new ObservableCollection<Character>();
        private const int _characterNumberPerBatch = 10;
        private int _currentCharacterIndex = 0;

        public House Overlord { get; set; } = null;
        public ObservableCollection<House> CadetBranches { get; } = new ObservableCollection<House>();

        private readonly Service<Character> _charactersService = Service<Character>.Instance;

        private bool IsFirstPage
        {
            get
            {
                return
                    _currentCharacterIndex == _characterNumberPerBatch - 1 && Item.SwornMembers.Length >= _characterNumberPerBatch ||
                    _currentCharacterIndex < _characterNumberPerBatch && Item.SwornMembers.Length < _characterNumberPerBatch && _currentCharacterIndex > 0;
            }
        }

        protected override async Task OnLoad()
        {
            await FetchHouses();
            await FetchCharacters();
        }

        private async Task FetchCharacters()
        {
            CurrentLord = await FetchCharacter(Item.CurrentLord);
            Heir = await FetchCharacter(Item.Heir);
            Founder = await FetchCharacter(Item.Founder);
            NotifyCharactersLoaded();

            if (Item.SwornMembers.Length == 0)
            {
                return;
            }
            await FetchNextBatch();
        }

        private async Task<Character> FetchCharacter(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            return await _charactersService.GetAsync(url);
        }

        private void NotifyCharactersLoaded()
        {
            FirePropertyChanged(new PropertyChangedEventArgs(nameof(CurrentLord)));
            FirePropertyChanged(new PropertyChangedEventArgs(nameof(Heir)));
            FirePropertyChanged(new PropertyChangedEventArgs(nameof(Founder)));
        }

        private async Task FetchHouses()
        {
            Overlord = await service.GetAsync(Item.Overlord);
            FirePropertyChanged(new PropertyChangedEventArgs(nameof(Overlord)));

            foreach (var houseUrl in Item.CadetBranches)
            {
                var house = await service.GetAsync(houseUrl);
                CadetBranches.Add(house);
            }
        }

        public async Task FetchNextBatch()
        {
            if (_currentCharacterIndex == Item.SwornMembers.Length - 1)
            {
                return;
            }

            if (SwornMembers.Count > 0)
            {
                SwornMembers.Clear();
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, nameof(SwornMembers), 0));
            }

            int startIndex = _currentCharacterIndex;
            while (_currentCharacterIndex < startIndex + _characterNumberPerBatch)
            {
                var character = await _charactersService.GetAsync(
                    new Uri(Item.SwornMembers[_currentCharacterIndex]));
                SwornMembers.Add(character);
                _currentCharacterIndex++;
                if (_currentCharacterIndex >= Item.SwornMembers.Length)
                {
                    _currentCharacterIndex = Item.SwornMembers.Length - 1;
                    break;
                }
            }
        }

        public async Task FetchPreviousBatch()
        {
            if (IsFirstPage)
            {
                return;
            }

            if (_currentCharacterIndex == Item.SwornMembers.Length - 1)
            {
                _currentCharacterIndex -= _currentCharacterIndex % _characterNumberPerBatch + _characterNumberPerBatch;
                await FetchNextBatch();
            }

            _currentCharacterIndex -= 2 * _characterNumberPerBatch;
            if (_currentCharacterIndex < 0)
            {
                _currentCharacterIndex += 2 * _characterNumberPerBatch;
                return;
            }

            await FetchNextBatch();
        }
    }
}
