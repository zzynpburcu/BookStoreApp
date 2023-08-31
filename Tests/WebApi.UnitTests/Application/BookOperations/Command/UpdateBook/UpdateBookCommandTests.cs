using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenInvalidBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {

            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.ID = 100;


            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap mevcut değil");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShoulBeUpdated()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookModel model = new UpdateBookModel() { Title = "Hobbit", PageCount = 1000, GenreId = 2};
            command.Model = model;
            command.ID=1;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(book => book.Id == command.ID);
            book.Should().NotBeNull();
            book.Title.Should().Be(model.Title);
            book.PageCount.Should().Be(model.PageCount);
            book.GenreId.Should().Be(model.GenreId);

        }

    }
}