using GoT_Wiki.Models;
using GoT_Wiki.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace GoT_Wiki.ViewModels
{
    /// <summary>
    /// Belongs to CharacterDetailsPage.
    /// Contains the logic and data for presentation.
    /// </summary>
    public class CharacterDetailsViewModel : DetailsViewModelBase<Character>
    {
        /// <summary>
        /// Father of the shown character.
        /// </summary>
        public Character Father { get; set; } = null;

        /// <summary>
        /// Mother of the shown character.
        /// </summary>
        public Character Mother { get; set; } = null;

        /// <summary>
        /// Spouse of the shown character.
        /// </summary>
        public Character Spouse { get; set; } = null;

        /// <summary>
        /// Holds all alliagences of the character.
        /// </summary>
        public ObservableCollection<House> Alliagences { get; } = new ObservableCollection<House>();

        /// <summary>
        /// Holds all books in which the character appears.
        /// </summary>
        public ObservableCollection<Book> Books { get; } = new ObservableCollection<Book>();

        /// <summary>
        /// Holds all books in which the character had a pov chapter.
        /// </summary>
        public ObservableCollection<Book> PovBooks { get; } = new ObservableCollection<Book>();

        /// <summary>
        /// Called whenever the ViewModel is loaded.
        /// Fetches all details of the character.
        /// </summary>
        protected override async Task OnLoad()
        {
            await FetchCharacters();
            await FetchHouses();
            await FetchBooks();
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

            var houseService = Service<House>.Instance;
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

            var bookService = Service<Book>.Instance;
            foreach (var bookUrl in urls)
            {
                var book = await bookService.GetAsync(bookUrl);
                books.Add(book);
            }
        }
    }
}
