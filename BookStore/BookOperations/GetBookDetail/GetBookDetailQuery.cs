using AutoMapper;
using BookStore.Common;
using BookStore.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public GetBookDetailQuery(IApplicationDbContext context,IMapper mapper)
        {
            _dbContext = context;   
            _mapper = mapper;   
        }
        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Include(x => x.Genres).Where(book => book.Id == BookId).FirstOrDefault();
            if(book is null)
            {
                throw new Exception("The book is not found");
            }
            BookDetailViewModel viewModel = _mapper.Map<BookDetailViewModel>(book);
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
