using GoT_Wiki.Models;

namespace GoT_Wiki.Services
{
    public class CharactersService : ServiceBase<Character>
    {
        public CharactersService() : base("api/characters") { }

        protected override void Process(Character item)
        {
            item.Name = string.IsNullOrEmpty(item.Name) ?
                item.Aliases[0] : item.Name;
        }
    }
}
