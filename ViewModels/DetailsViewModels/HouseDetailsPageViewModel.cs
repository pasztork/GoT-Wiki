using GoT_Wiki.Models;
using GoT_Wiki.Services;

namespace GoT_Wiki.ViewModels
{
    public class HouseDetailsPageViewModel : DetailsViewModelBase<House>
    {
        public HouseDetailsPageViewModel() : base(new HousesService()) { }
    }
}
