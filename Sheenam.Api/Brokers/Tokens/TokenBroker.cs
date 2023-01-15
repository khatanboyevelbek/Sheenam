// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Microsoft.IdentityModel.Tokens;
using Sheenam.Api.Models.Foundations.Guests;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Brokers.Tokens
{
    public class TokenBroker : ITokenBroker
    {
        private IConfiguration configuration;

        public TokenBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateJWT(Guest currentGuest)
        {
            var securityKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var cridentials =
                new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, currentGuest.Id.ToString()),
                new Claim(ClaimTypes.Email, currentGuest.Email)
            };

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cridentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateJWT(Host currentHost)
        {
            var securityKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var cridentials =
                new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, currentHost.Id.ToString()),
                new Claim(ClaimTypes.Email, currentHost.Email)
            };

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cridentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
