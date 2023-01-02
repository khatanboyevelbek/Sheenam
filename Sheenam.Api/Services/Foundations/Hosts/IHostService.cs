// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Services.Foundations.Hosts
{
    public interface IHostService
    {
        ValueTask<Host> AddHostAsync(Host host);
        IQueryable<Host> RetrieveAllHosts();
        ValueTask<Host> RetrieveHostByIdAsync(Guid id);
    }
}
