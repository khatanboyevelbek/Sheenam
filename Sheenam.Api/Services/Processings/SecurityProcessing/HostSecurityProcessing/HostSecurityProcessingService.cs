// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Services.Foundations.Security.HostSecurity;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Services.Processings.SecurityProcessing.HostSecurityProcessing
{
    public class HostSecurityProcessingService : IHostSecurityProcessingService
    {
        private readonly IHostSecurityService hostSecurityService;

        public HostSecurityProcessingService(IHostSecurityService hostSecurityService) =>
            this.hostSecurityService = hostSecurityService;

        public string CreateToken(Host currentHost) =>
            this.hostSecurityService.CreateToken(currentHost);
    }
}
