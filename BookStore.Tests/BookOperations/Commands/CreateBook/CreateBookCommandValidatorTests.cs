﻿using BookStore.BookOperations.CreateBooks;
using BookStore.Tests.TestsSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Tests.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord Of The Rings", 0, 0)]
        [InlineData("Lord Of The Rings", 0, 1)]
        [InlineData("Lord Of The Rings", 100, 0)]
        [InlineData("", 0, 0)]
        [InlineData("", 100, 1)]
        [InlineData("", 0, 1)]
        [InlineData(" ", 100, 1)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            /* Arrange */
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                GenreId = genreId,
                PublishDate = DateTime.Now.Date.AddYears(-1)
            };
            /* Act */
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);   

            /* Assert */
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_ShouldBeReturnError()
        {
            /* Arrange */
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "DenemeTestDeneme",
                PageCount = 200,
                GenreId = 1,
                PublishDate = DateTime.Now.Date
            };
            /* Act */
            CreateBookCommandValidator validate = new CreateBookCommandValidator();
            var error = validate.Validate(command);
            /* Assert */
            error.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "DenemeTestDenemeDebene",
                PageCount = 200,
                GenreId = 1,
                PublishDate = DateTime.Now.Date.AddYears(-2)
            };

            CreateBookCommandValidator validate = new CreateBookCommandValidator();
            var error = validate.Validate(command);

            error.Errors.Count.Should().Be(0);
        }
    }
}
