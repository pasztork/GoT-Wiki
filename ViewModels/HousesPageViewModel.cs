using GoT_Wiki.Models;
using GoT_Wiki.Services;

namespace GoT_Wiki.ViewModels
{
    public class HousesPageViewModel : ViewModelBase<House>
    {
        public HousesPageViewModel() : base(new HousesService()) { }
    }
}
