using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
           _context = testFixture.Context;
        }
       [Fact]
       public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationExeption_ShouldBeReturn()
       {
        //arrange (Hazırlık)
        var genre = new Genre() { Name=""};
        _context.Genres.Add(genre);
        _context.SaveChanges();

        CreateGenreCommand command = new CreateGenreCommand(_context);
        command.Model= new CreateGenreModel() {Name = genre.Name};

        // act & assert (Çalıştırma - Doğrulama)

        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü zaten mevcut");
       }

       [Fact]
       public void WhenValidInputsAreGiven_Genre_ShoulBeCreated()
       {
        //arrange
        CreateGenreCommand command = new CreateGenreCommand(_context);
        CreateGenreModel model = new CreateGenreModel() { Name ="Fiction"};
        command.Model = model;

        //act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        //assert
        var genre =_context.Genres.SingleOrDefault( genre => genre.Name == model.Name);
        genre.Should().NotBeNull();

       }
    }
}