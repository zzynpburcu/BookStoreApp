using System;
using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor( command => command.ID).GreaterThan(0);
            RuleFor(command => command.Model.Name).MinimumLength(3).NotEqual("string");
            RuleFor(command => command.Model.Surname).MinimumLength(2).NotEqual("string");
            RuleFor(command => command.Model.Date.Date).LessThan(DateTime.Now.Date);
        }
    }
}