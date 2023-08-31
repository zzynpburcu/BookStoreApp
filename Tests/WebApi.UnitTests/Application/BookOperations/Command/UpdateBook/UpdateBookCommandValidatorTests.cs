using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       [Theory]
       [InlineData("Lord of the rings",0,0,-1)]
       [InlineData("Lord of the rings",0,0,1)]
       [InlineData("",0,0,100)]
       [InlineData("",100,0,0)]
       [InlineData("Lord of the rings",100,1,0)]
       [InlineData("Lor",100,1,1)]
       [InlineData("Lord",0,1,0)]
       [InlineData(" ",100,1,8)]
       public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string title,int pageCount, int genreId,int bookid)
       {
        //arrange
        UpdateBookCommand command = new UpdateBookCommand(null);
        command.ID=bookid;
        command.Model = new UpdateBookModel(){
            Title=title,
            PageCount=pageCount,
            GenreId=genreId,
        };
        //act
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);

        //assert
        result.Errors.Count.Should().BeGreaterThan(0);
       }
    
     [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
         UpdateBookCommand command = new UpdateBookCommand(null);
         command.ID=1;
        command.Model = new UpdateBookModel(){
            Title="Lord of The Rings",
            PageCount=100,
            GenreId=1,
        };

        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}}