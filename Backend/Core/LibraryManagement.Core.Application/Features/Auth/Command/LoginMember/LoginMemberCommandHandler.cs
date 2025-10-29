using BC = BCrypt.Net.BCrypt;
using LibraryManagement.Core.Application.Features.Auth.Command.LoginMember;
using LibraryManagement.Core.Application.Interfaces.Tokens;
using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace LibraryManagement.Core.Application.Features.Auth.Commands.LoginMember
{
    public class LoginMemberCommandHandler : IRequestHandler<LoginMemberCommandRequest, LoginMemberCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public LoginMemberCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<LoginMemberCommandResponse> Handle(LoginMemberCommandRequest request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetReadRepository<Member>();
            var member = await repo.GetAsync(
                x => x.Email == request.Email && !x.IsDeleted,
                enableTracking: false);

            if (member == null)
                throw new Exception("Kullanıcı bulunamadı!");

            if (member.IsLocked)
                throw new Exception("Hesabınız kilitli. Lütfen yöneticiyle iletişime geçin.");

            bool passwordValid = BC.Verify(request.Password, member.PasswordHash);
            if (!passwordValid)
                throw new Exception("Geçersiz şifre!");

            var token = await _tokenService.CreateToken(member); // roller yoksa boş gönder
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginMemberCommandResponse
            {
                Token = tokenString,
                FullName = member.FullName,
                Email = member.Email,
                Expiration = token.ValidTo,
                Id = member.Id,

                Role = member.Role        
            };
        }
    }
}
