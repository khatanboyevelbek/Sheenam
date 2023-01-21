// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Brokers.Tokens;
using Sheenam.Api.Models.Foundations.Guests;

namespace Sheenam.Api.Services.Foundations.Security.GuestSecurity
{
    public class GuestSecurityService : IGuestSecurityService
    {
        private readonly ITokenBroker tokenBroker;

        public GuestSecurityService(ITokenBroker tokenBroker) =>
            this.tokenBroker = tokenBroker;

        public string CreateToken(Guest currentGuest) =>
            this.tokenBroker.GenerateJWT(currentGuest);
    }
}
