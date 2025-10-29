using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Authors.Command.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommandRequest>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(x => x.Id)
    .NotEmpty().GreaterThan(0).WithMessage("Geçerli bir Yazar Id giriniz.")
;


            RuleFor(x => x.FirstName)
    .NotEmpty().WithMessage("Yazar adı boş olamaz.")
    .MaximumLength(50).WithMessage("Yazar adı 50 karakterden uzun olamaz.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Yazar soyadı boş olamaz.")
                .MaximumLength(50).WithMessage("Yazar soyadı 50 karakterden uzun olamaz.");
        }
    }
}
