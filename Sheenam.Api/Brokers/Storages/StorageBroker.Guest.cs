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

        public async ValueTask<Guest> InsertGuestAsync(Guest guest) =>
            await InsertAsync(guest);

        public IQueryable<Guest> SelectAllGuests() =>
            SelectAll<Guest>();

        public async ValueTask<Guest> SelectGuestByIdAsync(Guid id) =>
            await SelectAsync<Guest>(id);

        public async ValueTask<Guest> UpdateGuestAsync(Guest guest) => 
            await UpdateAsync(guest);

        public async ValueTask<Guest> DeleteGuestAsync(Guest guest) =>
            await DeleteAsync(guest);
    }
}
