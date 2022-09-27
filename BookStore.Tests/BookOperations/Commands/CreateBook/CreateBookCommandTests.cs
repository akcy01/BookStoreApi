using AutoMapper;
using BookStore.BookOperations.CreateBooks;
using BookStore.Data;
using BookStore.Models;
using BookStore.Tests.TestsSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Tests.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext __context;

        private readonly IMapper __mapper;
        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            __context = testFixture._context;
            __mapper = testFixture._mapper;
        }
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()//tests generally use the void type
        {
            /* Arrange(Preparation) */
            var book = new Book() { Title = "TestTitle", PageCount = 100, GenreId = 2, PublishYear = new System.DateTime(1990, 11, 12) };
            __context.Books.Add(book); 
            __context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(__context, __mapper);
            command.Model = new CreateBookModel() { Title = book.Title };

            /* Assert & Act */
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The book is already exist.");
        } 
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            /* Arrange */
            CreateBookCommand command = new CreateBookCommand(__context,__mapper);
            CreateBookModel model = new CreateBookModel() { Title = "DenemeTitleDenemeDebene", PageCount = 1000, GenreId = 2, PublishDate = DateTime.Now.AddYears(-2) };
            command.Model = model;

            /* Act */
            FluentActions.Invoking(() => command.Handle()).Invoke();

            /* Assert */
            var book = __context.Books.SingleOrDefault(book => book.Title == model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            //book.PublishYear.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}
