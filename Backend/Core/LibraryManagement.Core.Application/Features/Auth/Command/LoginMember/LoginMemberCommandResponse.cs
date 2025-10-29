namespace LibraryManagement.Core.Application.Features.Auth.Command.LoginMember
{
    public class LoginMemberCommandResponse
    {
        public string Token { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime Expiration { get; set; }

        public int Id { get; set; }

        public string Role { get; set; } // 🔥 Eklendi
    }
}
