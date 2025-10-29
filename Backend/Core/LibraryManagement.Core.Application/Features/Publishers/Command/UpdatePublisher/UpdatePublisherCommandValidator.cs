using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Publishers.Command.UpdatePublisher
{
    public class UpdatePublisherCommandValidator : AbstractValidator<UpdatePublisherCommandRequest>
    {
        public UpdatePublisherCommandValidator()
        {
            RuleFor(x => x.Id)
.NotEmpty();

            RuleFor(x => x.Name)
.NotEmpty();

            RuleFor(x => x.Address)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
