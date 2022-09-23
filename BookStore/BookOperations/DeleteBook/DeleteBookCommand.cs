using BookStore.Data;

namespace BookStore.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly IApplicationDbContext _dbContext;
        public int BookId { get; set; }
        public DeleteBookCommand(IApplicationDbContext context)
        {
            _dbContext = context;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if(book is null)
            {
                throw new InvalidOperationException("The book has not found"); 
            }
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}
