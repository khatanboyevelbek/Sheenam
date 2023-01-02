// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Host> Hosts { get; set; }

        public async ValueTask<Host> InsertHostAsync(Host host)
        {
            var broker = new StorageBroker(this.configuration);
            broker.Entry(host).State = EntityState.Added;
            await broker.SaveChangesAsync();
            return host;
        }

        public IQueryable<Host> SelectAllHosts()
        {
            var broker = new StorageBroker(this.configuration);
            return broker.Set<Host>();
        }

        public async ValueTask<Host> SelectHostByIdAsync(Guid id)
        {
            var broker = new StorageBroker(this.configuration);
            var currentHost = await broker.Hosts.FindAsync(id);
            return currentHost;
        }

        public async ValueTask<Host> UpdateHostAsync(Host host)
        {
            var broker = new StorageBroker(this.configuration);
            broker.Entry(host).State = EntityState.Modified;
            await broker.SaveChangesAsync();
            return host;
        }

        public async ValueTask<Host> DeleteHostAsync(Host host)
        {
            var broker = new StorageBroker(this.configuration);
            broker.Entry(host).State = EntityState.Deleted;
            await broker.SaveChangesAsync();
            return host;
        }
    }
}
