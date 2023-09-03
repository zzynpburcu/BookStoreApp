using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       [Theory]
       [InlineData("Lor","Lor")]
       [InlineData(" "," ")]
       public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string name,string surname)
       {
        //arrange
        UpdateAuthorCommand command = new UpdateAuthorCommand(null);
        command.Model = new UpdateAuthorModel(){
            Name=name,
            Surname=surname
        };
        //act
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);

        //assert
        result.Errors.Count.Should().BeGreaterThan(0);
       }
    
     [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
         UpdateAuthorCommand command = new UpdateAuthorCommand(null);
         command.ID=2;
        command.Model = new UpdateAuthorModel(){
            Name="Jose",
            Surname="Saramago",
            Date = DateTime.Now.AddYears(-40)
        };

        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}}