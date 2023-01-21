// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Models.Foundations.Guests;

namespace Sheenam.Api.Services.Processings.SecurityProcessing.GuestSecurityPocessing
{
    public interface IGuestSecurityProcessingService
    {
        string CreateToken(Guest currentUser);
    }
}
