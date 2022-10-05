using AutoMapper;
using BookStore.BookOperations.GetBookDetail;
using BookStore.Data;
using BookStore.Tests.TestsSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Tests.BookOperations.Commands.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _mapper = testFixture._mapper;
            _context = testFixture._context;
        }
        [Fact]
        public void WhenInvalidInputIsGiven_Query_ShouldBeReturnExceptionAndMessage()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.BookId = -1;

            FluentActions.Invoking(() => query.Handle()).Should().Throw<Exception>().And.Message.Should().Be("The book is not found");
        }
        [Fact]
        public void WhenValidInputIsGiven_Query_ShouldBeReturnBookDetail()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.BookId = 2;

            FluentActions.Invoking(() => query.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(b => b.Id == query.BookId);
            book.Should().NotBeNull();
        }
    }
}
