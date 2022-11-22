// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Models.Foundations.Guests;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Guest> InsertGuestAsync(Guest guest);
    }
}
