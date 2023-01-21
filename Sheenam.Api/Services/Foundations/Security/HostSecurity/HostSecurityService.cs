// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Brokers.Tokens;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Services.Foundations.Security.HostSecurity
{
    public class HostSecurityService : IHostSecurityService
    {
        private readonly ITokenBroker tokenBroker;

        public HostSecurityService(ITokenBroker tokenBroker) =>
            this.tokenBroker = tokenBroker;

        public string CreateToken(Host currentHost) =>
            this.tokenBroker.GenerateJWT(currentHost);
    }
}
