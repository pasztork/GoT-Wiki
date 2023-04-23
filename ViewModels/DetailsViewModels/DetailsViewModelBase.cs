using GoT_Wiki.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace GoT_Wiki.ViewModels
{
    /// <summary>
    /// Base class for all view models belonging to a details page.
    /// </summary>
    /// <typeparam name="TClass">
    /// The type of the entity being shown.
    /// </typeparam>
    public abstract class DetailsViewModelBase<TClass> : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets called when any of the properties get changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private TClass _item = default;

        /// <summary>
        /// Used for setting and getting the shown entity.
        /// </summary>
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

        /// <summary>
        /// Loads <para>element</para>.
        /// </summary>
        /// <param name="elemenet">
        /// The entity we want to show.
        /// </param>
        public async Task Load(TClass elemenet)
        {
            Item = elemenet;
            await OnLoad();
        }

        /// <summary>
        /// Must be overriden by descendats.
        /// Used for initializing the ViewModel.
        /// </summary>
        /// <returns></returns>
        protected abstract Task OnLoad();

        /// <summary>
        /// Used by descendants if the shown entity changes.
        /// </summary>
        /// <param name="e">
        /// Contains the name of the changed property.
        /// </param>
        protected void FirePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
