// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.LoginModel;

namespace Sheenam.Api.Services.Processings.GuestProcessing
{
    public interface IGuestProcessingService
    {
        Guest RetrieveGuestByCridentials(LoginModel loginModel);
    }
}
