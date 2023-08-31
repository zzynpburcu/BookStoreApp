using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using Xunit;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 100;


            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü mevcut değil");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShoulBeUpdated()
        {
            //arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId=2;
            UpdateGenreModel model = new UpdateGenreModel() { Name = "Test"};
            command.Model = model;
           

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var genre = _context.Genres.SingleOrDefault(genre => genre.ID == command.GenreId);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);

        }

    }
}