using System;
using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor( command => command.ID).GreaterThan(0);
        }
    }
}