using BookStore.Data;
using BookStore.Models;

namespace BookStore.BookOperations.CreateBooks
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly ApplicationDbContext _dbContext;
        public CreateBookCommand(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if(book is not null)
            {
                throw new InvalidOperationException("The book is already exist.");
            }
            book = new Book();
            book.Title = Model.Title;
            book.PublishYear = Model.PublishDate;
            book.PageCount = Model.PageCount;
            book.GenreId = Model.GenreId;
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }
    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
