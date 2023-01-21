// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Services.Processings.SecurityProcessing.HostSecurityProcessing
{
    public interface IHostSecurityProcessingService
    {
        string CreateToken(Host currentHost);
    }
}
