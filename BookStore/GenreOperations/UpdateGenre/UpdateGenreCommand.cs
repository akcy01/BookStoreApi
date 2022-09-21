using BookStore.Data;

namespace BookStore.GenreOperations.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly ApplicationDbContext _context;

        public UpdateGenreCommand(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("The genre has not found");
            }
            if(_context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
            {
                throw new InvalidOperationException("The is already exist");
            }
            genre.Name = Model.Name.Trim() == default ? Model.Name : genre.Name;
            genre.IsActive = Model.IsActive;
            _context.SaveChanges();
        }
    }
    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
