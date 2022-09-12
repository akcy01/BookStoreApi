using BookStore.Common;
using BookStore.Data;

namespace BookStore.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly ApplicationDbContext _dbContext;
        public int BookId { get; set; }
        public GetBookDetailQuery(ApplicationDbContext context)
        {
            _dbContext = context;   
        }
        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(book => book.Id == BookId).FirstOrDefault();
            if(book is null)
            {
                throw new Exception("The book is not found");
            }
            BookDetailViewModel viewModel = new BookDetailViewModel();
            viewModel.Title = book.Title;
            viewModel.PageCount = book.PageCount;
            viewModel.PublishDate = book.PublishYear.Date.ToString("mm/dd/yy");
            viewModel.Genre = ((GenreEnum)book.GenreId).ToString();
            return viewModel;
        }
    }
    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}
