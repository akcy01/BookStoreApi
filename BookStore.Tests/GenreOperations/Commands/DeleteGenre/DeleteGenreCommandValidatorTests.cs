using BookStore.BookOperations.DeleteBook;
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
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext __context;
        public DeleteGenreCommandValidatorTests(CommonTestFixture testFixture)
        {
            __context = testFixture._context;
        }
        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(__context);
            command.GenreId = -3;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
