using AutoMapper;
using BookStore.Data;
using BookStore.Models;

namespace BookStore.BookOperations.CreateBooks
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookCommand(ApplicationDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if(book is not null)
            {
                throw new InvalidOperationException("The book is already exist.");
            }
            book = _mapper.Map<Book>(Model);
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
