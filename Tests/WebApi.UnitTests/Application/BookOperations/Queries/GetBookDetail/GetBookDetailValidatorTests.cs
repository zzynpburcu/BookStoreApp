using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookById;
using Xunit;

namespace Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookByIdQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShoulNotBeReturnError()
        {
            //arrange
            GetBookByIdQuery query = new GetBookByIdQuery(null, null);
            query.ID = 1;

            GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().Be(0);
        }
    }
}