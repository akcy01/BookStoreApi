using AutoMapper;
using BookStore.BookOperations.UpdateBooks;
using BookStore.Data;
using BookStore.Tests.TestsSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Tests.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext __context;
        private readonly IMapper __mapper;
        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            __context = testFixture._context;
            __mapper = testFixture._mapper;
        }
        [Fact]
        public void WhenInvalidInputIsGiven_Command_ShouldBeReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(__context);
            command.BookId = -3;

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The book has not found");
        }
        [Fact]
        public void WhenValidInputIsGiven_Command_ShouldBeUpdate()
        {
            UpdateBookCommand command = new UpdateBookCommand(__context);
            UpdateBookModel model = new UpdateBookModel() { Title = "DenemTest", GenreId = 1 };
            command.Model = model;
            command.BookId = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = __context.Books.SingleOrDefault(x => x.Title == model.Title);
            book.Should().NotBeNull();
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}
