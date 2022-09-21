using AutoMapper;
using BookStore.Data;
using BookStore.GenreOperations.CreateGenre;
using BookStore.GenreOperations.DeleteGenre;
using BookStore.GenreOperations.GetGenreDetail;
using BookStore.GenreOperations.GetGenres;
using BookStore.GenreOperations.UpdateGenre;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;

        public GenreController(IMapper mapper, ApplicationDbContext context)
        {
            this.mapper = mapper;
            _context = context;
        }
        [HttpGet]
        public ActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context, mapper);
            var obj = query.Handle();
            return Ok(obj);
        }
        [HttpGet("{id}")]
        public ActionResult GetGenreDetail(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, mapper);
            query.GenreId = id;
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var obj = query.Handle();
            return Ok(obj);
        }
        [HttpPost]
        public IActionResult AddGenre(CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = newGenre;
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();  
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id,UpdateGenreModel updateGene)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = id;
            command.Model = updateGene;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}
