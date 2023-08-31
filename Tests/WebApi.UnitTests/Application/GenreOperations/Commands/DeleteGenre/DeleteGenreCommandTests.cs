using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {

            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 0;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü mevcut değil");
        }
        [Fact]
        public void WhenValidGenreIdGiven_Genre_ShouldBeDeleted()
        {
            //Arrange
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId=3;

            //Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert
            var genre = _context.Genres.SingleOrDefault( genre => genre.ID == command.GenreId);
            genre.Should().Be(null);
        } 
    }
}