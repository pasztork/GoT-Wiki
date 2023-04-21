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

        private readonly ServiceBase<Book> _booksService = ServiceBase<Book>.Instance;
        private readonly ServiceBase<Character> _charactersService = ServiceBase<Character>.Instance;
        private readonly ServiceBase<House> _housesService = ServiceBase<House>.Instance;
        private string _query = string.Empty;

        public async Task Load(string queryString)
        {
            _query = queryString;
            await Load(_booksService, Books);
            await Load(_charactersService, Characters);
            await Load(_housesService, Houses);
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
