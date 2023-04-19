﻿using GoT_Wiki.Services;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace GoT_Wiki.ViewModels
{
    public abstract class ViewModelBase<TClass> : INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ObservableCollection<TClass> Collection { get; } = new ObservableCollection<TClass>();

        private int _pageNumber = 0;
        private readonly int _pageSize = 10;
        private readonly ServiceBase<TClass> _service = null;

        public ViewModelBase(ServiceBase<TClass> service)
        {
            _service = service;
            _service.PageSize = _pageSize;
            _ = InitTask();
        }

        private async Task InitTask()
        {
            await FetchNextPage();
        }

        public async Task FetchNextPage()
        {
            if (Collection.Count < _pageSize && _pageNumber > 0)
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
            var elements = await _service.GetAsync(_pageNumber);
            ClearCollection();
            foreach (var element in elements)
            {
                Process(element);
                AddCharacterToCollection(element);
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

        protected virtual void Process(TClass element) { }
    }
}