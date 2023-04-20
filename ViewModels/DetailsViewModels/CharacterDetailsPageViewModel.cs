using GoT_Wiki.Models;
using GoT_Wiki.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace GoT_Wiki.ViewModels
{
    public class CharacterDetailsViewModel : DetailsViewModelBase<Character>
    {
        public Character Father { get; set; } = null;
        public Character Mother { get; set; } = null;
        public Character Spouse { get; set; } = null;
        public ObservableCollection<House> Alliagences { get; } = new ObservableCollection<House>();
        public ObservableCollection<Book> Books { get; } = new ObservableCollection<Book>();
        public ObservableCollection<Book> PovBooks { get; } = new ObservableCollection<Book>();

        public CharacterDetailsViewModel() : base(new CharactersService()) { }

        protected override void OnLoad()
        {
            _ = FetchCharacters();
            _ = FetchHouses();
            _ = FetchBooks();
        }

        private async Task FetchCharacters()
        {
            Father = await FetchCharacter(Item.Father);
            Mother = await FetchCharacter(Item.Mother);
            Spouse = await FetchCharacter(Item.Spouse);
            NotifyCharactersLoaded();
        }

        private async Task<Character> FetchCharacter(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }
            return await service.GetAsync(url);
        }

        private void NotifyCharactersLoaded()
        {
            FirePropertyChanged(new PropertyChangedEventArgs(nameof(Father)));
            FirePropertyChanged(new PropertyChangedEventArgs(nameof(Mother)));
            FirePropertyChanged(new PropertyChangedEventArgs(nameof(Spouse)));
        }

        private async Task FetchHouses()
        {
            if (Item.Allegiances.Length == 0)
            {
                return;
            }

            var houseService = new HousesService();
            foreach (var houseUrl in Item.Allegiances)
            {
                var house = await houseService.GetAsync(houseUrl);
                Alliagences.Add(house);
            }
        }

        private async Task FetchBooks()
        {
            await FetchBooks(Books, Item.Books);
            await FetchBooks(PovBooks, Item.PovBooks);
        }

        private async Task FetchBooks(ObservableCollection<Book> books, string[] urls)
        {
            if (urls.Length == 0)
            {
                return;
            }

            var bookService = new BooksService();
            foreach (var bookUrl in urls)
            {
                var book = await bookService.GetAsync(bookUrl);
                books.Add(book);
            }
        }
    }
}
