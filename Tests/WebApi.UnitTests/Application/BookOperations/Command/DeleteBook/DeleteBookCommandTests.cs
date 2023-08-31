using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenInvalidBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.ID = 0;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap mevcut değil");
        }
        [Fact]
        public void WhenValidBookIdGiven_Book_ShouldBeDeleted()
        {
            //Arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.ID=1;

            //Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert
            var book = _context.Books.SingleOrDefault( book => book.Id == command.ID);
            book.Should().Be(null);
        } 
    }
}