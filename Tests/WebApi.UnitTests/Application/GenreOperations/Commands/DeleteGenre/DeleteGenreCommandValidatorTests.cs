 using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       [Theory]
       [InlineData(-1)]
       [InlineData(0)]
       public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int genreId)
       {
        //arrange
        DeleteGenreCommand command = new DeleteGenreCommand(null);
        command.GenreId =genreId;
        //act
        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);

        //assert
        result.Errors.Count.Should().BeGreaterThan(0);
       }
    
   
     [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
         DeleteGenreCommand command = new DeleteGenreCommand(null);
        command.GenreId = 1;

        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}}