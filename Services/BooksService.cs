using GoT_Wiki.Models;

namespace GoT_Wiki.Services
{
    public class BooksService : ServiceBase<Book>
    {
        public BooksService() : base("api/books") { }
    }
}
