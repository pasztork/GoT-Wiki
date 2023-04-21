using GoT_Wiki.Services;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace GoT_Wiki.ViewModels
{
    public class ListViewModelBase<TClass> : INotifyCollectionChanged
    {
        private static ListViewModelBase<TClass> _instance;
        public static ListViewModelBase<TClass> Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ListViewModelBase<TClass>();
                }
                return _instance;
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ObservableCollection<TClass> Collection { get; } = new ObservableCollection<TClass>();

        private static int _pageNumber = 1;
        private static readonly int _pageSize = 10;
        private readonly ServiceBase<TClass> _service = ServiceBase<TClass>.Instance;

        public ListViewModelBase()
        {
            _service.PageSize = _pageSize;
            _ = InitTask();
        }

        private async Task InitTask()
        {
            await LoadPage();
        }

        public async Task FetchNextPage()
        {
            if (Collection.Count < _pageSize && _pageNumber > 1)
            {
                return;
            }

            _pageNumber++;
            await LoadPage();
        }

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
