using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Members.Command.UpdateMember
{
    public class UpdateMemberCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
        public string MemberCode { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
