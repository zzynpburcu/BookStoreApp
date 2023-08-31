using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       [Theory]
       [InlineData(-1)]
       [InlineData(0)]
       public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int bookId)
       {
        //arrange
        DeleteBookCommand command = new DeleteBookCommand(null);
        command.ID =bookId;
        //act
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        var result = validator.Validate(command);

        //assert
        result.Errors.Count.Should().BeGreaterThan(0);
       }
    
   
     [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
         DeleteBookCommand command = new DeleteBookCommand(null);
        command.ID = 1;

        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}}