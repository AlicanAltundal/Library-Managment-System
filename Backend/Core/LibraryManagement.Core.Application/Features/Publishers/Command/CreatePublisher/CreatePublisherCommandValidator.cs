using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Publishers.Command.CreatePublisher
{
    public class CreatePublisherCommandValidator : AbstractValidator<CreatePublisherCommandRequest> 
    {
        public CreatePublisherCommandValidator()
        {
            RuleFor(x => x.Name)
.NotEmpty().WithMessage("MemberCode alanı zorunludur.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Tam ad zorunludur.")
                .MaximumLength(200);
        }
    }
}
