// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Services.Foundations.Security.PasswordHash;

namespace Sheenam.Api.Services.Foundations.Guests;

public partial class GuestService : IGuestService
{
    private readonly IStorageBroker storageBroker;
    private readonly ILoggingBroker loggingBroker;
    private readonly IPasswordHashServise passwordHashServise;

    public GuestService(IStorageBroker storageBroker,
        ILoggingBroker loggingBroker,
        IPasswordHashServise passwordHashServise)
    {
        this.storageBroker = storageBroker;
        this.loggingBroker = loggingBroker;
        this.passwordHashServise = passwordHashServise;
    }

    public ValueTask<Guest> AddGuestAsync(Guest guest) =>
        TryCatch(async () =>
        {
            ValidateGuestOnAdd(guest);

            guest.Password = 
                this.passwordHashServise.GenerateHashPassword(guest.Password);

            guest.CreatedDate = DateTimeOffset.UtcNow;
            guest.UpdatedDate = DateTimeOffset.UtcNow;

            return await this.storageBroker.InsertGuestAsync(guest);
        });

    public IQueryable<Guest> RetrieveAllGuests() =>
        TryCatch(() => this.storageBroker.SelectAllGuests());

    public ValueTask<Guest> RetrieveGuestByIdAsync(Guid id) =>
        TryCatch(async () =>
        {
            ValidateGuestId(id);
            return await this.storageBroker.SelectGuestByIdAsync(id);
        });

    public ValueTask<Guest> ModifyGuestAsync(Guest guest)
    {
        return TryCatch(async () =>
        {
            ValidateGuestOnModify(guest);

            guest.Password =
                this.passwordHashServise.GenerateHashPassword(guest.Password);

            guest.UpdatedDate = DateTimeOffset.UtcNow;

            return await this.storageBroker.UpdateGuestAsync(guest);
        });
    }

    public ValueTask<Guest> RemoveGuestAsync(Guid id)
    {
        return TryCatch(async () =>
        {
            ValidateGuestId(id);

            Guest gettingGuest =
                await this.storageBroker.SelectGuestByIdAsync(id);

            return await this.storageBroker.DeleteGuestAsync(gettingGuest);
        });
    }
}
