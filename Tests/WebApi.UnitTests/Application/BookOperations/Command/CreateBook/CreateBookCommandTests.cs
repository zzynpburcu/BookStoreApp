using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
           _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
       [Fact]
       public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationExeption_ShouldBeReturn()
       {
        //arrange (Hazırlık)
        var book = new Book() { Title="", PageCount=100,PublishDate=new System.DateTime(2005,06,01),GenreId=1,AuthorId=1};
        _context.Books.Add(book);
        _context.SaveChanges();

        CreateBookCommand command = new CreateBookCommand(_context,_mapper);
        command.Model= new CreateBookModel() {Title = book.Title};

        // act & assert (Çalıştırma - Doğrulama)

        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut");
       }

       [Fact]
       public void WhenValidInputsAreGiven_Book_ShoulBeCreated()
       {
        //arrange
        CreateBookCommand command = new CreateBookCommand(_context,_mapper);
        CreateBookModel model = new CreateBookModel() { Title ="Lord of the Rings", PageCount=1000,PublishDate=DateTime.Now.AddYears(-10),GenreId=2,AuthorId=1};
        command.Model = model;

        //act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        //assert
        var book =_context.Books.SingleOrDefault( book => book.Title == model.Title);
        book.Should().NotBeNull();
        book.PageCount.Should().Be(model.PageCount);
        book.PublishDate.Should().Be(model.PublishDate);
        book.GenreId.Should().Be(model.GenreId);
        book.AuthorId.Should().Be(model.AuthorId);

       }
    }
}