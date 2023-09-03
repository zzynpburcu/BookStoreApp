using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
           _context = testFixture.Context;
        }
       [Fact]
       public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationExeption_ShouldBeReturn()
       {
        //arrange (Hazırlık)
        var author = new Author() { Name="", Surname=""};
        _context.Authors.Add(author);
        _context.SaveChanges();

        CreateAuthorCommand command = new CreateAuthorCommand(_context);
        command.Model= new CreateAuthorModel() {Name = author.Name, Surname = author.Surname};

        // act & assert (Çalıştırma - Doğrulama)

        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut");
       }

       [Fact]
       public void WhenValidInputsAreGiven_Author_ShoulBeCreated()
       {
        //arrange
        CreateAuthorCommand command = new CreateAuthorCommand(_context);
        CreateAuthorModel model = new CreateAuthorModel() { Name ="Jack London"};
        command.Model = model;

        //act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        //assert
        var author =_context.Authors.SingleOrDefault( author => author.Name == model.Name);
        author.Should().NotBeNull();

       }
    }
}