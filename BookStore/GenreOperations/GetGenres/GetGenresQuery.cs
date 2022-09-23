using AutoMapper;
using BookStore.Data;

namespace BookStore.GenreOperations.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetGenresQuery(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        { 
            var genres  = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id);
            List<GenresViewModel> obj = _mapper.Map<List<GenresViewModel>>(genres);
            return obj;
        }
    }

    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
