// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Models.Foundations.Guests;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Brokers.Tokens
{
    public interface ITokenBroker
    {
        string GenerateJWT(Guest currentUser);
        string GenerateJWT(Host currentHost);
    }
}
