using BookStore.Common;
using BookStore.Data;

namespace BookStore.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly ApplicationDbContext _dbContext;
        public GetBooksQuery(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList();
            List<BooksViewModel> result = new List<BooksViewModel>();
            foreach (var book in bookList)
            {
                result.Add(new BooksViewModel()
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PublishDate = book.PublishYear.Date.ToString("dd/mm/yyyy"),
                    PageCount = book.PageCount
                });
            }
            return result;
        }
    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }

    }
}
