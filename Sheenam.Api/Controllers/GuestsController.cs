// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Helpers.Tokens;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Guests.Exceptions;
using Sheenam.Api.Models.Foundations.LoginModel;
using Sheenam.Api.Services.Foundations.Guests;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GuestsController : RESTFulController
    {
        private readonly IGuestService guestService;
        private readonly IConfiguration configuration;
        private readonly IGenerateToken generateToken;

        public GuestsController(IGuestService guestService,
            IConfiguration configuration, IGenerateToken generateToken)
        {
            this.guestService = guestService;
            this.configuration = configuration;
            this.generateToken = generateToken;
        }

        private string GenerateHashPassword(string password)
        {
            byte[] passwordHash;

            using (var hmacsha = SHA256.Create())
            {
                passwordHash =
                    hmacsha.ComputeHash(Encoding.Default.GetBytes(password));
            };

            return Convert.ToBase64String(passwordHash);
        }

        private string GetCurrentGuest()
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
        public async ValueTask<ActionResult<Guest>> PostGuestAsync(Guest guest)
        {
            try
            {
                Guest postedGuest = await this.guestService.AddGuestAsync(guest);
                return Created(postedGuest);
            }
            catch (GuestValidationException guestValidationException)
            {
                return BadRequest(guestValidationException.InnerException);
            }
            catch (GuestDependencyValidationException guestDependencyValidationException)
                when (guestDependencyValidationException.InnerException is AlreadyExistGuestException)

            {
                return Conflict(guestDependencyValidationException.InnerException);
            }
            catch (GuestDependencyValidationException guestDependencyValidationException)
            {
                return BadRequest(guestDependencyValidationException.InnerException);
            }
            catch (GuestDependencyException guestDependencyException)
            {
                return InternalServerError(guestDependencyException.InnerException);
            }
            catch (GuestDependencyServiceException guestDependencyServiceException)
            {
                return InternalServerError(guestDependencyServiceException.InnerException);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<string> LoginGuest(LoginModel loginModel)
        {
            try
            {
                Guest? currentGuest =
                    this.guestService.RetrieveAllGuests().FirstOrDefault(
                    guest => guest.Email.Trim().ToLower() == loginModel.Email.Trim().ToLower()
                    && guest.Password == GenerateHashPassword(loginModel.Password));

                if (currentGuest is not null)
                {
                    string generatedJwtToken = generateToken.GenerateJwtToken(currentGuest);

                    return Ok(new {GuestId = currentGuest.Id, Token = generatedJwtToken});
                }
                else
                {
                    throw new FailedGuestLoginException();
                }
            }
            catch (FailedGuestLoginException failedUserLoginException)
            {
                return BadRequest(failedUserLoginException);
            }
            catch (GuestDependencyException guestDependencyException)
            {
                return InternalServerError(guestDependencyException.InnerException);
            }
            catch (GuestDependencyServiceException guestDependencyServiceException)
            {
                return InternalServerError(guestDependencyServiceException.InnerException);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async ValueTask<ActionResult<Guest>> GetGuestByIdAsync([FromRoute] Guid id)
        {
            try
            {
                var authorizedGuestId = GetCurrentGuest();

                if (authorizedGuestId == id.ToString())
                {
                    Guest currentGuest =
                        await this.guestService.RetrieveGuestByIdAsync(id);

                    return Ok(currentGuest);
                }
                else
                {
                    throw new ForbiddenGuestException();
                }
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                var unauthorizedGuestException = new UnauthorizedGuestException();
                return Unauthorized(unauthorizedGuestException);
            }
            catch (ForbiddenGuestException forbiddenGuestException)
            {
                return Forbidden(forbiddenGuestException);
            }
            catch (GuestValidationException guestValidationException)
            {
                return BadRequest(guestValidationException.InnerException);
            }
            catch (GuestDependencyException guestDependencyException)
            {
                return InternalServerError(guestDependencyException.InnerException);
            }
            catch (GuestDependencyServiceException guestDependencyServiceException)
            {
                return InternalServerError(guestDependencyServiceException.InnerException);
            }
        }

        [HttpPut("update")]
        [Authorize]
        public async ValueTask<ActionResult<Guest>> PutGuestAsync([FromBody] Guest guest)
        {
            try
            {
                var authorizedGuestId = GetCurrentGuest();
                
                if(authorizedGuestId == guest.Id.ToString())
                {
                    Guest updatedGuest =
                        await this.guestService.ModifyGuestAsync(guest);

                    return Ok(updatedGuest);
                }
                throw new ForbiddenGuestException();

            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                var unauthorizedGuestException = new UnauthorizedGuestException();
                return Unauthorized(unauthorizedGuestException);
            }
            catch (ForbiddenGuestException forbiddenGuestException)
            {
                return Forbidden(forbiddenGuestException);
            }
            catch (GuestValidationException guestValidationException)
            {
                return BadRequest(guestValidationException.InnerException);
            }
            catch (GuestDependencyValidationException guestDependencyValidationException)
                when (guestDependencyValidationException.InnerException is AlreadyExistGuestException)

            {
                return Conflict(guestDependencyValidationException.InnerException);
            }
            catch (GuestDependencyValidationException guestDependencyValidationException)
            {
                return BadRequest(guestDependencyValidationException.InnerException);
            }
            catch (GuestDependencyException guestDependencyException)
            {
                return InternalServerError(guestDependencyException.InnerException);
            }
            catch (GuestDependencyServiceException guestDependencyServiceException)
            {
                return InternalServerError(guestDependencyServiceException.InnerException);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async ValueTask<ActionResult<Guest>> DeleteGuestAsync([FromRoute] Guid id)
        {
            try
            {
                var authorizedGuestId = GetCurrentGuest();

                if (authorizedGuestId == id.ToString())
                {
                    Guest deletedGuest =
                        await this.guestService.RemoveGuestAsync(id);

                    return Ok(deletedGuest);
                }
                else
                {
                    throw new ForbiddenGuestException();
                }
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                var unauthorizedGuestException = new UnauthorizedGuestException();
                return Unauthorized(unauthorizedGuestException);
            }
            catch (ForbiddenGuestException forbiddenGuestException)
            {
                return Forbidden(forbiddenGuestException);
            }
            catch (GuestValidationException guestValidationException)
            {
                return BadRequest(guestValidationException.InnerException);
            }
            catch (GuestDependencyValidationException guestDependencyValidationException)
                when (guestDependencyValidationException.InnerException is AlreadyExistGuestException)

            {
                return Conflict(guestDependencyValidationException.InnerException);
            }
            catch (GuestDependencyValidationException guestDependencyValidationException)
            {
                return BadRequest(guestDependencyValidationException.InnerException);
            }
            catch (GuestDependencyException guestDependencyException)
            {
                return InternalServerError(guestDependencyException.InnerException);
            }
            catch (GuestDependencyServiceException guestDependencyServiceException)
            {
                return InternalServerError(guestDependencyServiceException.InnerException);
            }
        }
    }
}
