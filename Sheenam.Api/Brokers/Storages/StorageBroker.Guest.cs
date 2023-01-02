// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using EFxceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sheenam.Api.Models.Foundations.Guests;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker : EFxceptionsContext
    {
        public DbSet<Guest> Guests { get; set; }

        public async ValueTask<Guest> InsertGuestAsync(Guest? guest)
        {
            var broker = new StorageBroker(this.configuration);
            broker.Entry(guest).State = EntityState.Added;
            await broker.SaveChangesAsync();
            return guest;
        }

        public IQueryable<Guest> SelectAllGuests()
        {
            var broker = new StorageBroker(this.configuration);
            return broker.Set<Guest>();
        }

        public async ValueTask<Guest> SelectGuestByIdAsync(Guid id)
        {
            var broker = new StorageBroker(this.configuration);
            return await broker.Guests.FindAsync(id);
        }

        public async ValueTask<Guest> UpdateGuestAsync(Guest guest)
        {
            var broker = new StorageBroker(this.configuration);
            broker.Entry(guest).State = EntityState.Modified;
            await broker.SaveChangesAsync();
            return guest;
        }

        public async ValueTask<Guest> DeleteGuestAsync(Guest guest)
        {
            var broker = new StorageBroker(this.configuration);
            broker.Entry(guest).State = EntityState.Deleted;
            await broker.SaveChangesAsync();
            return guest;
        }
    }
}
