﻿using AutoMapper;
using BookStore.BookOperations.GetBooks;
using BookStore.Data;
using BookStore.GenreOperations.GetGenres;

namespace BookStore.GenreOperations.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQuery(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);
            if(genre is null)
            {
                throw new InvalidOperationException("The genre type has not found");
            }
            return _mapper.Map<GenreDetailViewModel>(genre);
        }
    }
    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
