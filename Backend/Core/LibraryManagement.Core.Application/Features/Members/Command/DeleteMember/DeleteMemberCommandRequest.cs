using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Members.Command.DeleteMember
{
    public class DeleteMemberCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
