using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Members.Command.CreateMember
{
    public class CreateMemberCommandRequest : IRequest<Unit>
    {
        public string MemberCode { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }

        public string Role { get; set; } = "User"; // "Admin" veya "User"

        public string Password { get; set; }   // ✅ kullanıcıdan gelen düz şifre
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        // Relationships
    }
}
