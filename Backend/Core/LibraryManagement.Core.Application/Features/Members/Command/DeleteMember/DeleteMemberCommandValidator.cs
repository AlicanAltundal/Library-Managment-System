using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Members.Command.DeleteMember
{
    public class DeleteMemberCommandValidator : AbstractValidator<DeleteMemberCommandRequest>
    {
        public DeleteMemberCommandValidator()
        {
            RuleFor(x => x.Id)
  .GreaterThan(0)
  .WithName("Üyenin ID'si 0'dan büyük olmalıdır.");
        }
    }
}
