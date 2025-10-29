using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Librarians.Command.CreateLibrarian
{
    public class CreateLibrarianCommandValidator : AbstractValidator<CreateLibrarianCommandRequest>
    {
        public CreateLibrarianCommandValidator()
        {

            RuleFor(x => x.EmployeeCode)
                .NotEmpty().WithMessage("ISBN zorunludur.");
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Kitap adı zorunludur.")
                .MaximumLength(35);

            RuleFor(x => x.Department)
                .NotEmpty().WithMessage("Tür alanı zorunludur.")
                .MaximumLength(30);

        }
    }
}
