// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Models.Foundations.Guests;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Services.Processings
{
    public interface IUserProcessingService
    {
        string CreateToken(Guest currentGuest);
        string CreateToken(Host currentHost);
    }
}
