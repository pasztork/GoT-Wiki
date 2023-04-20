using GoT_Wiki.Models;
using GoT_Wiki.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GoT_Wiki.ViewModels
{
    public class SearchResultViewModel
    {
        public ObservableCollection<Book> Books { get; } = new ObservableCollection<Book>();
        public ObservableCollection<Character> Characters { get; } = new ObservableCollection<Character>();
        public ObservableCollection<House> Houses { get; } = new ObservableCollection<House>();

        private readonly BooksService _booksService = new BooksService();
        private readonly CharactersService _charactersService = new CharactersService();
        private readonly HousesService _housesService = new HousesService();
        private string _query = string.Empty;

        public async Task Load(string queryString)
        {
            _query = queryString;
            await Load<Book>(_booksService, Books);
            await Load<Character>(_charactersService, Characters);
            await Load<House>(_housesService, Houses);
        }

        private async Task Load<TClass>(ServiceBase<TClass> service, ObservableCollection<TClass> collection)
        {
            var result = await service.GetByNameAsync(_query);
            foreach (var item in result)
            {
                collection.Add(item);
            }
        }
    }
}
