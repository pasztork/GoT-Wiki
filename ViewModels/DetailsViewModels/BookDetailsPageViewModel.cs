using GoT_Wiki.Models;
using GoT_Wiki.Services;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace GoT_Wiki.ViewModels
{
    public class BookDetailsPageViewModel : DetailsViewModelBase<Book>, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ObservableCollection<Character> Characters { get; } = new ObservableCollection<Character>();
        public ObservableCollection<Character> PovCharacters { get; } = new ObservableCollection<Character>();

        private const int _characterNumberPerBatch = 10;
        private int _currentCharacterIndex = 0;
        private int _currentPovCharacterIndex = 0;
        private readonly ServiceBase<Character> _characterService = new CharactersService();

        public BookDetailsPageViewModel() : base(new BooksService()) { }

        protected override async Task OnLoad()
        {
            await FetchNextBatch();
            await FetchNextPovCharacterBatch();
        }


        public async Task FetchNextBatch()
        {
            _currentCharacterIndex = await FetchNext(Characters, Item.Characters, _currentCharacterIndex);
        }

        public async Task FetchPreviousBatch()
        {
            _currentCharacterIndex = await FetchPrevious(Characters, Item.Characters, _currentCharacterIndex);
        }

        public async Task FetchNextPovCharacterBatch()
        {
            _currentPovCharacterIndex = await FetchNext(PovCharacters, Item.PovCharacters, _currentPovCharacterIndex);
        }

        public async Task FetchPreviousPovCharacterBatch()
        {
            _currentPovCharacterIndex = await FetchPrevious(PovCharacters, Item.PovCharacters, _currentPovCharacterIndex);
        }

        private async Task<int> FetchNext(ObservableCollection<Character> target, string[] source, int currentIndex)
        {
            if (currentIndex == source.Length - 1)
            {
                return currentIndex;
            }

            ClearCollection(target);
            int startIndex = currentIndex;
            while (currentIndex < startIndex + _characterNumberPerBatch)
            {
                var character = await _characterService.GetAsync(new Uri(source[currentIndex]));
                target.Add(character);
                currentIndex++;
                if (currentIndex >= source.Length)
                {
                    currentIndex = source.Length - 1;
                    break;
                }
            }
            return currentIndex;
        }

        private async Task<int> FetchPrevious(ObservableCollection<Character> target, string[] source, int currentIndex)
        {
            if (currentIndex == source.Length - 1)
            {
                currentIndex -= currentIndex % _characterNumberPerBatch + _characterNumberPerBatch;
                currentIndex = await FetchNext(target, source, currentIndex);
                return currentIndex;
            }

            currentIndex -= 2 * _characterNumberPerBatch;
            if (currentIndex < 0)
            {
                currentIndex += 2 * _characterNumberPerBatch;
                return currentIndex;
            }

            currentIndex = await FetchNext(target, source, currentIndex);
            return currentIndex;
        }

        private void ClearCollection(ObservableCollection<Character> collection)
        {
            collection.Clear();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, nameof(collection), 0));
        }
    }
}
