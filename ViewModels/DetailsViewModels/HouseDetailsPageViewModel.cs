using GoT_Wiki.Models;
using GoT_Wiki.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace GoT_Wiki.ViewModels
{
    public class HouseDetailsPageViewModel : DetailsViewModelBase<House>
    {
        public Character CurrentLord { get; set; } = null;
        public Character Heir { get; set; } = null;
        public Character Founder { get; set; } = null;

        public House Overlord { get; set; } = null;
        public ObservableCollection<House> CadetBranches { get; } = new ObservableCollection<House>();

        public HouseDetailsPageViewModel() : base(new HousesService()) { }

        protected override async Task OnLoad()
        {
            await FetchCharacters();
            await FetchHouses();
        }

        private async Task FetchCharacters()
        {
            CurrentLord = await FetchCharacter(Item.CurrentLord);
            Heir = await FetchCharacter(Item.Heir);
            Founder = await FetchCharacter(Item.Founder);
            NotifyCharactersLoaded();
        }

        private async Task<Character> FetchCharacter(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            var characterService = new CharactersService();
            return await characterService.GetAsync(url);
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
    }
}
