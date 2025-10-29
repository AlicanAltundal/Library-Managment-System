using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Authors.Command.DeleteAuthor
{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommandRequest>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(x => x.Id)
               .NotEmpty().GreaterThan(0).WithMessage("Geçerli Yazar Id giriniz.");
        }
    }
}
