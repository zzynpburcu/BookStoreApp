using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using Xunit;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       [Theory]
       [InlineData("Lor")]
       [InlineData(" ")]
       public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string name)
       {
        //arrange
        UpdateGenreCommand command = new UpdateGenreCommand(null);
        command.Model = new UpdateGenreModel(){
            Name=name
        };
        //act
        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);

        //assert
        result.Errors.Count.Should().BeGreaterThan(0);
       }
    
     [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
         UpdateGenreCommand command = new UpdateGenreCommand(null);
        command.Model = new UpdateGenreModel(){
            Name="Sitcom",
        };

        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}}