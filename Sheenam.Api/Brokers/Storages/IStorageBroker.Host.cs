// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Host> InsertHostAsync(Host host);
        IQueryable<Host> SelectAllHosts();
        ValueTask<Host> SelectHostByIdAsync(Guid id);
        ValueTask<Host> UpdateHostAsync(Host host);
    }
}
