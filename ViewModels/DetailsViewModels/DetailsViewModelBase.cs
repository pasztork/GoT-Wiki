using GoT_Wiki.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace GoT_Wiki.ViewModels
{
    public abstract class DetailsViewModelBase<TClass> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TClass _item = default;
        public TClass Item
        {
            get { return _item; }
            set
            {
                if (!EqualityComparer<TClass>.Default.Equals(_item, value))
                {
                    _item = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Item)));
                }
            }
        }

        protected readonly Service<TClass> service = Service<TClass>.Instance;

        public async Task Load(TClass elemenet)
        {
            Item = elemenet;
            await OnLoad();
        }

        protected abstract Task OnLoad();

        protected void FirePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
