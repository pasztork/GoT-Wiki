using GoT_Wiki.Models;
using GoT_Wiki.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GoT_Wiki.ViewModels
{
    /// <summary>
    /// ViewModel used with SearchResultPage.
    /// </summary>
    public class SearchResultViewModel
    {
        /// <summary>
        /// Holds the books that satisfy the search query.
        /// </summary>
        public ObservableCollection<Book> Books { get; } = new ObservableCollection<Book>();

        /// <summary>
        /// Holds the characters that satisfy the search query.
        /// </summary>
        public ObservableCollection<Character> Characters { get; } = new ObservableCollection<Character>();

        /// <summary>
        /// Holds the houses that satisfy the search query.
        /// </summary>
        public ObservableCollection<House> Houses { get; } = new ObservableCollection<House>();

        private readonly Service<Book> _booksService = Service<Book>.Instance;
        private readonly Service<Character> _charactersService = Service<Character>.Instance;
        private readonly Service<House> _housesService = Service<House>.Instance;
        private string _query = string.Empty;

        /// <summary>
        /// Initializes the class by findind all matching entities.
        /// </summary>
        /// <param name="queryString">
        /// The query (name) satisfied by shown entities.
        /// </param>
        public async Task Load(string queryString)
        {
            _query = queryString;
            await Load(_booksService, Books);
            await Load(_charactersService, Characters);
            await Load(_housesService, Houses);
        }

        private async Task Load<TClass>(Service<TClass> service, ObservableCollection<TClass> collection)
        {
            var result = await service.GetByNameAsync(_query);
            foreach (var item in result)
            {
                collection.Add(item);
            }
        }
    }
}
