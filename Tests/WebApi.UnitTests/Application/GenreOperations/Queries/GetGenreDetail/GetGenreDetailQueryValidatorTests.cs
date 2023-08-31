using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookById;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using Xunit;

namespace Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetBookByIdQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShoulNotBeReturnError()
        {
            //arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.GenreId = 1;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().Be(0);
        }
    }
}