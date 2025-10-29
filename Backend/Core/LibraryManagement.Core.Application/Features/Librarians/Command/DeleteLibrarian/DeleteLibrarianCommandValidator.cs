using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Librarians.Command.DeleteLibrarian
{
    public class DeleteLibrarianCommandValidator : AbstractValidator<DeleteLibrarianCommandRequest>
    {
        public DeleteLibrarianCommandValidator()
        {
            RuleFor(x => x.Id)
.NotEmpty().GreaterThan(0).WithMessage("Geçerli görevli adı girin.");

        }
    }
}
