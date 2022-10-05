using BookStore.BookOperations.DeleteBook;
using BookStore.Data;
using BookStore.Tests.TestsSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Tests.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext __context;
        public DeleteBookCommandValidatorTests(CommonTestFixture testFixture)
        {
            __context = testFixture._context;
        }
        [Fact]  
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors()
        {
            DeleteBookCommand command = new DeleteBookCommand(__context);
            command.BookId = -3;

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            DeleteBookCommand command = new DeleteBookCommand(__context);
            command.BookId = 3;

            FluentActions.Invoking(() => command.Handle()).Should().NotThrow();
        }
    }
}
