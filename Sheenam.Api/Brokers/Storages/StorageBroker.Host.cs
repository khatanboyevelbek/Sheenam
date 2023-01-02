﻿// ---------------------------------------------------
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
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Host> hostEntryEntity = await broker.Hosts.AddAsync(host);
            await broker.SaveChangesAsync();
            return hostEntryEntity.Entity;
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
    }
}
