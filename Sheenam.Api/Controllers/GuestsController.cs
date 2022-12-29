// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Guests.Exceptions;
using Sheenam.Api.Services.Foundations.Guests;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GuestsController : RESTFulController
    {
        private readonly IGuestService guestService;
        private readonly IConfiguration configuration;

        public GuestsController(IGuestService guestService, IConfiguration configuration)
        {
            this.guestService = guestService;
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
        public ActionResult<string> LoginGuestAsync(LoginModel loginModel)
        {
            try
            {
                Guest? currentGuest = 
                    this.guestService.RetrieveAllGuests().FirstOrDefault(
                        guest => guest.Email.Trim().ToLower() == loginModel.Email.Trim().ToLower() 
                        && guest.Password == CreatePasswordHash(loginModel.Password));

                if(currentGuest is not null)
                {
                    var securityKey = 
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

                    var cridentials = 
                        new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, currentGuest.Id.ToString()),
                        new Claim(ClaimTypes.Email, currentGuest.Email),
                    };

                    var token = new JwtSecurityToken(
                        configuration["Jwt:Issuer"],
                        configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.Now.AddHours(6),
                        signingCredentials: cridentials
                        );

                    string generatedToken = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(generatedToken);
                }
                else
                {
                    return NotFound("Guest is not found");
                }

                
            }
            catch (GuestDependencyException guestDependencyException)
            {
                return InternalServerError(guestDependencyException.InnerException);
            }
            catch(GuestDependencyServiceException guestDependencyServiceException)
            {
                return InternalServerError(guestDependencyServiceException.InnerException);
            }
        }
        
    }
}
