using GoT_Wiki.Models;
using GoT_Wiki.Services;

namespace GoT_Wiki.ViewModels
{
    public class HousesPageViewModel : ListViewModelBase<House>
    {
        public HousesPageViewModel() : base(new HousesService()) { }
    }
}
