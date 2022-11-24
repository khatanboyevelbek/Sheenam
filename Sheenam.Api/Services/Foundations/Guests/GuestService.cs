// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Guests;

namespace Sheenam.Api.Services.Foundations.Guests;

public class GuestService : IGuestService
{
    private readonly IStorageBroker storageBroker;

    public GuestService(IStorageBroker storageBroker)
    {
        this.storageBroker = storageBroker;
    }

    public async ValueTask<Guest> AddGuestAsync(Guest guest) =>
        await this.storageBroker.InsertGuestAsync(guest);
}
