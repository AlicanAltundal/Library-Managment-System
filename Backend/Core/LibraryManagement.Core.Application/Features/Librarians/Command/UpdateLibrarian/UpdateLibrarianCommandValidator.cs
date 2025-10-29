using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Librarians.Command.UpdateLibrarian
{
    public class UpdateLibrarianCommandValidator : AbstractValidator<UpdateLibrarianCommandRequest>
    {
        public UpdateLibrarianCommandValidator()
        {
            RuleFor(x => x.Id)
.NotEmpty().GreaterThan(0).WithMessage("Geçerli görevli adı girin.");

            RuleFor(x => x.EmployeeCode)
    .NotEmpty();
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(35);

            RuleFor(x => x.Department)
                .NotEmpty()
                .MaximumLength(30);

        }
    }
}
