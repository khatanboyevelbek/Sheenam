// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Brokers.Tokens;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;
using Sheenam.Api.Models.Foundations.LoginModel;
using Sheenam.Api.Services.Foundations.Hosts;
using Sheenam.Api.Services.Foundations.Security.PasswordHash;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class HostsController : RESTFulController
    {
        private readonly IHostService hostService;
        private readonly IConfiguration configuration;
        private readonly ITokenBroker generateToken;
        private readonly IPasswordHashServise passwordHashServise;

        public HostsController(IHostService hostService,
            IConfiguration configuration, ITokenBroker generateToken,
            IPasswordHashServise passwordHashServise)
        {
            this.hostService = hostService;
            this.configuration = configuration;
            this.generateToken = generateToken;
            this.passwordHashServise = passwordHashServise;
        }

        private string GetCurrentHost()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                string? Id = userClaims.FirstOrDefault(x => x.Type ==
                    ClaimTypes.NameIdentifier)?.Value;

                return Id;
            }
            throw new UnauthorizedAccessException();
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
        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult<string> LoginHost(LoginModel loginModel)
        {
            try
            {
                Host? currentHost = this.hostService.RetrieveAllHosts()
                    .FirstOrDefault(host => host.Email.Trim().ToLower() == 
                    loginModel.Email.Trim().ToLower()
                    && host.Password == this.passwordHashServise.GenerateHashPassword(loginModel.Password));

                if(currentHost is not null)
                {
                    string generatedJwtToken = generateToken.GenerateJWT(currentHost);

                    return Ok(new { HostId = currentHost.Id, Token = generatedJwtToken });
                }
                else
                {
                    throw new FailedHostLoginException();
                }
            }
            catch (FailedHostLoginException failedHostLoginException)
            {
                return BadRequest(failedHostLoginException);
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

        [HttpGet("{id}")]
        [Authorize]
        public async ValueTask<ActionResult<Host>> GetHostByIdAsync([FromRoute] Guid id)
        {
            try
            {
                var authorizedHostId = GetCurrentHost();

                if (authorizedHostId == id.ToString())
                {
                    Host currentHost = await this.hostService.RetrieveHostByIdAsync(id);
                    return Ok(currentHost);
                }
                else
                {
                    throw new ForbiddenHostException();
                }
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                var unauthorizedHostException = new UnauthorizedHostException();
                return Unauthorized(unauthorizedHostException);
            }
            catch (ForbiddenHostException forbiddenHostException)
            {
                return Forbidden(forbiddenHostException);
            }
            catch (HostValidationException hostValidationException)
            {
                return BadRequest(hostValidationException.InnerException);
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

        [HttpPut("update")]
        [Authorize]
        public async ValueTask<ActionResult<Host>> PutHostAsync([FromBody] Host host)
        {
            try
            {
                var authorizedGuestId = GetCurrentHost();

                if (authorizedGuestId == host.Id.ToString())
                {
                    Host updatedHost =
                        await this.hostService.ModifyHostAsync(host);

                    return Ok(updatedHost);
                }
                else
                {
                    throw new ForbiddenHostException();
                }
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                var unauthorizedHostException = new UnauthorizedHostException();
                return Unauthorized(unauthorizedHostException);
            }
            catch (ForbiddenHostException forbiddenHostException)
            {
                return Forbidden(forbiddenHostException);
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

        [HttpDelete("{id}")]
        [Authorize]
        public async ValueTask<ActionResult<Host>> DeleteHostAsync([FromRoute] Guid id)
        {
            try
            {
                var authorizedHostId = GetCurrentHost();

                if (authorizedHostId == id.ToString())
                {
                    Host deletedHost = await this.hostService.RemoveHostAsync(id);
                    return Ok(deletedHost);
                }
                else
                {
                    throw new ForbiddenHostException();
                }
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                var unauthorizedHostException = new UnauthorizedHostException();
                return Unauthorized(unauthorizedHostException);
            }
            catch (ForbiddenHostException forbiddenHostException)
            {
                return Forbidden(forbiddenHostException);
            }
            catch (HostValidationException hostValidationException)
            {
                return BadRequest(hostValidationException.InnerException);
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
    }
}
