using AutoMapper;
using BookStore.BookOperations.DeleteBook;
using BookStore.Data;
using BookStore.Models;
using BookStore.Tests.TestsSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Tests.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext __context;
        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            __context = testFixture._context;
        }

        [Fact]
        /* When Id has not found,Is it sending error ? */
        public void WhenBookIdNotFound_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            DeleteBookCommand command = new DeleteBookCommand(__context);
            command.BookId = 999;
            //Act & Assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The book has not found");
        }

        [Fact]
        /* When Id is valid,stated book should be remove */
        public void WhenValidIdIsGiven_Book_ShouldBeRemove()
        {
            DeleteBookCommand command = new DeleteBookCommand(__context);
            command.BookId = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = __context.Books.SingleOrDefault(book => book.Id == command.BookId);
            book.Should().BeNull();
        }
    }
}
