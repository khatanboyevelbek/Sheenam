// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Services.Foundations.Security;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Services.Processings.Security
{
    public class UserProcessingService : IUserProcessingService
    {
        private readonly ISecurityService securityService;

        public UserProcessingService(ISecurityService securityService) =>
             this.securityService = securityService;

        public string CreateToken(Guest currentGuest) =>
            securityService.CreateToken(currentGuest);

        public string CreateToken(Host currentHost) =>
            securityService.CreateToken(currentHost);
    }
}
