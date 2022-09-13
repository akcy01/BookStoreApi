using BookStore.Data;

namespace BookStore.BookOperations.UpdateBooks
{
    public class UpdateBookCommand
    {
        private readonly ApplicationDbContext _dbContext;
        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }
        public UpdateBookCommand(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
            {  
                throw new InvalidOperationException("The book has not found");
            }
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _dbContext.SaveChanges();
        }
    }
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
    }
}
