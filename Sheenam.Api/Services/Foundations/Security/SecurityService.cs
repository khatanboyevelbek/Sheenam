// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Brokers.Tokens;
using Sheenam.Api.Models.Foundations.Guests;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Services.Foundations.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly ITokenBroker tokenBroker;

        public SecurityService(ITokenBroker tokenBroker) =>
            this.tokenBroker = tokenBroker;

        public string CreateToken(Guest currentGuest) =>
            this.tokenBroker.GenerateJWT(currentGuest);

        public string CreateToken(Host currentHost) =>
            this.tokenBroker.GenerateJWT(currentHost);
    }
}
