using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Books.Command.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommandRequest>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(x => x.Id)
              .GreaterThan(0)
              .WithName("Kitabın ID'si 0'dan büyük olmalıdır.");
        }
    }
}
