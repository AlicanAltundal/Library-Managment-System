using LibraryManagement.Core.Application.Interfaces.Tokens;
using LibraryManagement.Core.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Infrastructure.Tokens
{
    public class TokenService : ITokenService
    {


        private readonly TokenSettings tokenSettings;

        public TokenService(IOptions<TokenSettings> options)
        {
            tokenSettings = options.Value;
        }

        public async Task<JwtSecurityToken> CreateToken(Member member)
        {
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, member.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, member.Email),
        new Claim(ClaimTypes.Name, member.FullName),
        new Claim(ClaimTypes.Role, member.Role)

    };

        

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: tokenSettings.Issuer,
                audience: tokenSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(tokenSettings.TokenValidityInMinutes),
                signingCredentials: creds
            );


            return token;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[62];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidIssuer = tokenSettings.Issuer,
                ValidAudience = tokenSettings.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Secret)),
                ValidateLifetime = false // We want to get claims from expired tokens as well
            };

            JwtSecurityTokenHandler tokenHandler = new();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken
                || !jwtSecurityToken.Header.Alg
                .Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;



        }
    }
}
