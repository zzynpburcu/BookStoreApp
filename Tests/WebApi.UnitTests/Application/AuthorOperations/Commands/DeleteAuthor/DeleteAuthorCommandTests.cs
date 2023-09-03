using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
       
        DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
        //son eklenen yazarın ID si
        var lastAuthor = _context.Authors.Max(author => author.Id);
        command.ID = lastAuthor+1;

        // act & assert (Çalıştırma - Doğrulama)
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar mevcut değil");
        }
         [Fact]
        public void WhenAttemptToDeleteAuthorWithBooks_InvalidOperationException_ShouldBeReturn()
        {

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.ID = 2;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitabı mevcut olan bir yazar silinemez.Yazarı silmek için lütfen önce kitabını siliniz.");
        }
        [Fact]
        public void WhenValidAuthorIdGiven_Author_ShouldBeDeleted()
        {
            //Arrange
           
            var author = new Author() { Name="Lewis", Surname="Carroll"};
        _context.Authors.Add(author);
        _context.SaveChanges();

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            var lastAuthor = _context.Authors.SingleOrDefault(x => x.Name == "Lewis");
            command.ID = lastAuthor.Id;


            //Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert
            var authorr = _context.Authors.SingleOrDefault( author => author.Id == command.ID);
            authorr.Should().Be(null);
        } 
    }
}