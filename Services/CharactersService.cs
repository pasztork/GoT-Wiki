using GoT_Wiki.Models;

namespace GoT_Wiki.Services
{
    public class CharactersService : ServiceBase<Character>
    {
        public CharactersService() : base("api/characters") { }
    }
}
