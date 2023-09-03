 using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       [Theory]
       [InlineData(-1)]
       [InlineData(0)]
       public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int authorId)
       {
        //arrange
        DeleteAuthorCommand command = new DeleteAuthorCommand(null);
        command.ID =authorId;
        //act
        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        var result = validator.Validate(command);

        //assert
        result.Errors.Count.Should().BeGreaterThan(0);
       }
    
   
     [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
         DeleteAuthorCommand command = new DeleteAuthorCommand(null);
        command.ID = 1;

        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}}