using GoT_Wiki.Services;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace GoT_Wiki.ViewModels
{
    /// <summary>
    /// Base class for all view models belonging to a page 
    /// showing a list of the same type of entities.
    /// </summary>
    /// <typeparam name="TClass">
    /// The type of the entities being shown.
    /// </typeparam>
    public class ListViewModel<TClass> : INotifyCollectionChanged
    {
        private static ListViewModel<TClass> _instance;

        /// <summary>
        /// There is only one instance of this class for each type.
        /// That instance can be accessed with this property.
        /// </summary>
        /// <remarks>
        /// Uses lazy initialization.
        /// </remarks>
        public static ListViewModel<TClass> Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ListViewModel<TClass>();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Called whenever the shown list changes.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Holds the entities that are shown.
        /// </summary>
        public ObservableCollection<TClass> Collection { get; } = new ObservableCollection<TClass>();

        private static int _pageNumber = 1;
        private static readonly int _pageSize = 10;
        private readonly Service<TClass> _service = Service<TClass>.Instance;

        /// <summary>
        /// Public ctor.
        /// Sets page size for the service of the same type and
        /// initilizes the page.
        /// </summary>
        public ListViewModel()
        {
            _service.PageSize = _pageSize;
            _ = InitTask();
        }

        private async Task InitTask()
        {
            await LoadPage();
        }

        /// <summary>
        /// Fetches next _pageSize pieces of entities.
        /// </summary>
        public async Task FetchNextPage()
        {
            if (Collection.Count < _pageSize && _pageNumber > 1)
            {
                return;
            }

            _pageNumber++;
            await LoadPage();
        }

        /// <summary>
        /// Fetches previouse _pageSize of entities.
        /// </summary>
        /// <returns></returns>
        public async Task FetchPreviousPage()
        {
            if (_pageNumber == 1)
            {
                return;
            }

            _pageNumber--;
            await LoadPage();
        }

        private async Task LoadPage()
        {
            var items = await _service.GetAsync(_pageNumber);
            ClearCollection();
            foreach (var element in items)
            {
                AddCharacterToCollection(element);
            }

            if (Collection.Count == 0)
            {
                await FetchPreviousPage();
            }
        }

        private void ClearCollection()
        {
            Collection.Clear();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, Collection, 0));
        }

        private void AddCharacterToCollection(TClass element)
        {
            Collection.Add(element);
            CollectionChanged?.Invoke(this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, Collection, Collection.Count - 1));
        }
    }
}
