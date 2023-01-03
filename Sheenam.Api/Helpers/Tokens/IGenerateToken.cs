// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Models.Foundations.Guests;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Helpers.Tokens
{
    public interface IGenerateToken
    {
        string GenerateJwtToken(Guest currentUser);
        string GenerateJwtToken(Host currentHost);
    }
}
