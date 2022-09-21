using BookStore.Data;

namespace BookStore.GenreOperations.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly ApplicationDbContext _context;
        public DeleteGenreCommand(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if(genre is null)
            {
                throw new InvalidOperationException("The book has not found");
            }
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }
    }
}
