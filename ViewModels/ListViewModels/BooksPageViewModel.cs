using GoT_Wiki.Models;
using GoT_Wiki.Services;

namespace GoT_Wiki.ViewModels
{
    public class BooksPageViewModel : ListViewModelBase<Book>
    {
        public BooksPageViewModel() : base(new BooksService()) { }
    }
}
