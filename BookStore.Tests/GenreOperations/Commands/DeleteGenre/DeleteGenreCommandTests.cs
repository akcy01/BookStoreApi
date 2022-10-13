using AutoMapper;
using BookStore.Data;
using BookStore.GenreOperations.DeleteGenre;
using BookStore.Tests.TestsSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Tests.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext __context;
        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            __context = testFixture._context;
        }
        [Fact]
        public void WhenGenreIdIsNotFound_InvalidOperationExceptionShouldBeReturn()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(__context);
            command.GenreId = 999;

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The book has not found");
        }
    }
}
