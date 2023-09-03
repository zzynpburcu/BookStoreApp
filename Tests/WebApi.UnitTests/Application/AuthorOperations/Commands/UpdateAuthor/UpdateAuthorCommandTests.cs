using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.ID = 100;


            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar mevcut değil");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Author_ShoulBeUpdated()
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.ID=2;
            UpdateAuthorModel model = new UpdateAuthorModel() { Name = "Test"};
            command.Model = model;
           

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var author = _context.Authors.SingleOrDefault(author => author.Id == command.ID);
            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            

        }

    }
}