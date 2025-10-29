using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Publishers.Command.DeletePublisher
{
    public class DeletePublisherCommandValidator : AbstractValidator<DeletePublisherCommandRequest>
    {
        public DeletePublisherCommandValidator()
        {
            RuleFor(x => x.Id)
.GreaterThan(0)
.WithName("Yayıncı ID'si 0'dan büyük olmalıdır.");
        }
    }
}
