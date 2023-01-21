// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Services.Foundations.Security.GuestSecurity;

namespace Sheenam.Api.Services.Processings.SecurityProcessing.GuestSecurityPocessing
{
    public class GuestSecurityProcessingService : IGuestSecurityProcessingService
    {
        private readonly IGuestSecurityService guestSecurityService;

        public GuestSecurityProcessingService(IGuestSecurityService guestSecurityService) =>
            this.guestSecurityService = guestSecurityService;

        public string CreateToken(Guest currentGuest) =>
            guestSecurityService.CreateToken(currentGuest);
    }
}
