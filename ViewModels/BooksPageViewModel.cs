using GoT_Wiki.Models;
using GoT_Wiki.Services;

namespace GoT_Wiki.ViewModels
{
    public class BooksPageViewModel : ViewModelBase<Book>
    {
        public BooksPageViewModel() : base(new BooksService()) { }
    }
}
