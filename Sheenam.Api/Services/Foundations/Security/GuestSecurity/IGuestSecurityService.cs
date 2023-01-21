// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Models.Foundations.Guests;

namespace Sheenam.Api.Services.Foundations.Security.GuestSecurity
{
    public interface IGuestSecurityService
    {
        string CreateToken(Guest currentGuest);
    }
}
