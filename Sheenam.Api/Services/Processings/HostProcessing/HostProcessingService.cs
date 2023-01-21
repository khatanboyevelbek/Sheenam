// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Host = Sheenam.Api.Models.Foundations.Hosts.Host;
using Sheenam.Api.Services.Foundations.Hosts;
using Sheenam.Api.Models.Foundations.LoginModel;

namespace Sheenam.Api.Services.Processings.HostProcessing
{
    public class HostProcessingService : IHostProcessingService
    {
        private readonly IHostService hostService;

        public HostProcessingService(IHostService hostService) =>
            this.hostService = hostService;

        public Host? RetrieveHostByCridentials(LoginModel loginModel)
        {
            return this.hostService.RetrieveAllHosts().
                FirstOrDefault(host => host.Email.Equals(loginModel.Email)
                && host.Password.Equals(loginModel.Password));
        }
    }
}
