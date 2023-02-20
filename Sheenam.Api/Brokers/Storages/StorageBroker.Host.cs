// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Host> Hosts { get; set; }

        public async ValueTask<Host> InsertHostAsync(Host host) =>
            await InsertAsync(host);

        public IQueryable<Host> SelectAllHosts() =>
            SelectAll<Host>();

        public async ValueTask<Host> SelectHostByIdAsync(Guid id) =>
            await SelectAsync<Host>(id);

        public async ValueTask<Host> UpdateHostAsync(Host host) =>
            await UpdateAsync(host);

        public async ValueTask<Host> DeleteHostAsync(Host host) =>
            await DeleteAsync(host);
    }
}
