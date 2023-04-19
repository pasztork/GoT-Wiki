using GoT_Wiki.Models;
using GoT_Wiki.Services;

namespace GoT_Wiki.ViewModels
{
    public class CharactersPageViewModel : ListViewModelBase<Character>
    {
        public CharactersPageViewModel() : base(new CharactersService()) { }

        protected override void Process(Character element)
        {
            element.Name = string.IsNullOrEmpty(element.Name) ?
                element.Aliases[0] : element.Name;
        }
    }
}
