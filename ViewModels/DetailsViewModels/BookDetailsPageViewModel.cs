using GoT_Wiki.Models;
using GoT_Wiki.Services;

namespace GoT_Wiki.ViewModels
{
    public class BookDetailsPageViewModel : DetailsViewModelBase<Book>
    {
        public BookDetailsPageViewModel() : base(new BooksService()) { }
    }
}
