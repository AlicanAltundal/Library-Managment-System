using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Members.Command.CreateMember
{
    public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommandRequest>
    {
        public CreateMemberCommandValidator()
        {

            RuleFor(x => x.Role)
        .NotEmpty().WithMessage("Rol belirtilmelidir.")
        .Must(r => r == "Admin" || r == "User")

        .WithMessage("Role sadece 'Admin' veya 'User' olabilir.");


            RuleFor(x => x.MemberCode)
    .NotEmpty().WithMessage("MemberCode alanı zorunludur.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Tam ad zorunludur.")
                .MaximumLength(200);

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres alanı zorunludur.")
                .MaximumLength(100);

            RuleFor(x => x.PhoneNumber)
                .Length(10)
                .WithMessage("Telefon numarası 10 hane (5XX XXX XX XX) yapısında olmalıdır.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email adresi zorunludur.");
        }
    }
}
