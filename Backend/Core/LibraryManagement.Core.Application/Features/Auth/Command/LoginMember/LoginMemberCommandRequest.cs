using MediatR;

namespace LibraryManagement.Core.Application.Features.Auth.Command.LoginMember
{
    public class LoginMemberCommandRequest : IRequest<LoginMemberCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }


    }
}
