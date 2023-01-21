// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.LoginModel;
using Sheenam.Api.Services.Foundations.Guests;

namespace Sheenam.Api.Services.Processings.GuestProcessing
{
    public class GuestProcessingService : IGuestProcessingService
    {
        private readonly IGuestService guestService;

        public GuestProcessingService(IGuestService guestService) =>
            this.guestService = guestService;

        public Guest? RetrieveGuestByCridentials(LoginModel loginModel)
        {
            return this.guestService.RetrieveAllGuests()
                   .FirstOrDefault(guest => guest.Email.Equals(loginModel.Email) 
                   && guest.Password.Equals(loginModel.Password));
        }
    }
}
