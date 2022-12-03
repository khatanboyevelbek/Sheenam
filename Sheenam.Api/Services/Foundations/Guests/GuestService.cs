// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Guests;

namespace Sheenam.Api.Services.Foundations.Guests;

public class GuestService : IGuestService
{
    private readonly IStorageBroker _storageBroker;
    private readonly ILoggingBroker _loggingBroker;

    public GuestService(IStorageBroker storageBroker, 
        ILoggingBroker loggingBroker)
    {
        this._storageBroker = storageBroker;
        this._loggingBroker = loggingBroker;
    }

    public async ValueTask<Guest> AddGuestAsync(Guest? guest) =>
        await this._storageBroker.InsertGuestAsync(guest);
}
