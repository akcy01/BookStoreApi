using AutoMapper;
using BookStore.Data;
using BookStore.GenreOperations.CreateGenre;
using BookStore.Models;
using BookStore.Tests.TestsSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Tests.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext __context;
        private readonly IMapper __Mapper; 

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            __context = testFixture._context;
            __Mapper = testFixture._mapper;
        }

        [Fact]
        public void WhenAlreadyExistNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var genre = new Genre() { Name = "DenemeGenre", IsActive = true };
            __context.Genres.Add(genre);
            __context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(__context);
            command.Model = new CreateGenreModel() { Name = genre.Name };

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The genre type is already exist");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            CreateGenreCommand command = new CreateGenreCommand(__context);
            CreateGenreModel model = new CreateGenreModel() { Name = "DenemeGenreDeneme" };
            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre = __context.Genres.SingleOrDefault(u => u.Name == model.Name);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
        }
    }
}
