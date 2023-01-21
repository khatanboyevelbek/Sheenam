// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Microsoft.IdentityModel.Tokens;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Processings.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Brokers.Tokens
{
    public class TokenBroker : ITokenBroker
    {
        private readonly TokenConfiguration tokenConfiguration;

        public TokenBroker(IConfiguration configuration)
        {
            tokenConfiguration = new TokenConfiguration();
            configuration.Bind("Jwt", tokenConfiguration);
        }

        public string GenerateJWT(Guest currentGuest)
        {
            byte[] convertKeyToBytes = 
                Encoding.UTF8.GetBytes(tokenConfiguration.Key);

            var securityKey =
                        new SymmetricSecurityKey(convertKeyToBytes);

            var cridentials =
                new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, currentGuest.Id.ToString()),
                new Claim(ClaimTypes.Email, currentGuest.Email)
            };

            var token = new JwtSecurityToken(
                tokenConfiguration.Issuer,
                tokenConfiguration.Audience,
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cridentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateJWT(Host currentHost)
        {
            byte[] convertKeyToBytes =
                Encoding.UTF8.GetBytes(tokenConfiguration.Key);

            var securityKey =
                        new SymmetricSecurityKey(convertKeyToBytes);

            var cridentials =
                new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, currentHost.Id.ToString()),
                new Claim(ClaimTypes.Email, currentHost.Email)
            };

            var token = new JwtSecurityToken(
                tokenConfiguration.Issuer,
                tokenConfiguration.Audience,
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cridentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
