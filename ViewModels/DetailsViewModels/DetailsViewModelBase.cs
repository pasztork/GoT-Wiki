using GoT_Wiki.Services;
using System.Collections.Generic;
using System.ComponentModel;

namespace GoT_Wiki.ViewModels
{
    public abstract class DetailsViewModelBase<TClass> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TClass _element = default;
        public TClass Element
        {
            get { return _element; }
            set
            {
                if (!EqualityComparer<TClass>.Default.Equals(_element, value))
                {
                    _element = value;
                    Process(_element);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Element)));
                }
            }
        }

        private readonly ServiceBase<TClass> _service = null;

        public DetailsViewModelBase(ServiceBase<TClass> service)
        {
            _service = service;
        }

        public void Load(TClass elemenet)
        {
            Element = elemenet;
        }

        protected virtual void Process(TClass element) { }
    }
}
