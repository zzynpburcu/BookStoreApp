using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       [Theory]
       [InlineData(" "," ")]
       [InlineData("","")]
       [InlineData("tes","tes")]
       public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string name,string surname)
       {
        //arrange
        CreateAuthorCommand command = new CreateAuthorCommand(null);
        command.Model = new CreateAuthorModel(){
            Name=name,
        };
        //act
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        //assert
        result.Errors.Count.Should().BeGreaterThan(0);
       }
    
     [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        CreateAuthorCommand command = new CreateAuthorCommand(null);
        command.Model = new CreateAuthorModel(){
            Name="Test",
            Surname="Test",
            Date=System.DateTime.Now.AddYears(-40)
        };

        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}}