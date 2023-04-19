using GoT_Wiki.Models;

namespace GoT_Wiki.Services
{
    public class HousesService : ServiceBase<House>
    {
        public HousesService() : base("api/houses") { }
    }
}
