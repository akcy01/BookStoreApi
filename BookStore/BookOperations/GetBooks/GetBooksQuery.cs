using AutoMapper;
using BookStore.Common;
using BookStore.Data;
using BookStore.Models;

namespace BookStore.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(ApplicationDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
            List<BooksViewModel> result = _mapper.Map<List<BooksViewModel>>(bookList);
            return result;
        }
    }
    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishYear { get; set; }
        public string Genre { get; set; }
    }
}
