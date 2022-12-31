// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;
using Sheenam.Api.Models.Foundations.LoginModel;
using Sheenam.Api.Services.Foundations.Hosts;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class HostsController : RESTFulController
    {
        private readonly IHostService hostService;
        private readonly IConfiguration configuration;

        public HostsController(IHostService hostService,
            IConfiguration configuration)
        {
            this.hostService = hostService;
            this.configuration = configuration;
        }

        private string CreatePasswordHash(string password)
        {
            byte[] passwordHash;

            using (var hmacsha = SHA256.Create())
            {
                passwordHash = hmacsha.ComputeHash(Encoding.Default.GetBytes(password));
            };

            return Convert.ToBase64String(passwordHash);
        }

        private string GenerateJwtToken(Host currentHost)
        {
            var securityKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var cridentials =
                new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                        new Claim(ClaimTypes.NameIdentifier, currentHost.Id.ToString()),
                        new Claim(ClaimTypes.Email, currentHost.Email),
                    };

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(6),
                signingCredentials: cridentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("register")]
        public async ValueTask<ActionResult<Host>> PostHostAsync(Host host)
        {
            try
            {
                Host postedHost = await this.hostService.AddHostAsync(host);
                return Created(postedHost);
            }
            catch (HostValidationException hostValidationException)
            {
                return BadRequest(hostValidationException.InnerException);
            }
            catch (HostDependencyValidationException hostDependencyValidationException)
                when (hostDependencyValidationException.InnerException is AlreadyExistHostException)
            {
                return Conflict(hostDependencyValidationException.InnerException);
            }
            catch (HostDependencyValidationException hostDependencyValidationException)
            {
                return BadRequest(hostDependencyValidationException.InnerException);
            }
            catch (HostDependencyException hostDependencyException)
            {
                return InternalServerError(hostDependencyException.InnerException);
            }
            catch (HostDependencyServiceException hostDependencyServiceException)
            {
                return InternalServerError(hostDependencyServiceException.InnerException);
            }
        }

        [HttpPost("Login")]
        public ActionResult<string> LoginHost(LoginModel loginModel)
        {
            try
            {
                Host? currentHost = this.hostService.RetrieveAllHosts()
                    .FirstOrDefault(host => host.Email.Trim().ToLower() == 
                        loginModel.Email.Trim().ToLower()
                        && host.Password == CreatePasswordHash(loginModel.Password));

                if(currentHost is not null)
                {
                    string generatedJwtToken = GenerateJwtToken(currentHost);

                    var tokenObject = new
                    {
                        Token = generatedJwtToken
                    };

                    return Ok(JsonSerializer.Serialize(tokenObject));
                }
                else
                {
                    throw new FailedHostLoginException();
                }
            }
            catch (FailedHostLoginException failedHostLoginException)
            {
                return Unauthorized(failedHostLoginException);
            }
            catch(HostDependencyException hostDependencyException)
            {
                return InternalServerError(hostDependencyException.InnerException);
            }
            catch(HostDependencyServiceException hostDependencyServiceException)
            {
                return InternalServerError(hostDependencyServiceException.InnerException);
            }
        }
    }
}
