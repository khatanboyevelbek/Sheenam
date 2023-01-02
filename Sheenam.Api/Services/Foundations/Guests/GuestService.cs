// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using System.Security.Cryptography;
using System.Text;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Guests;

namespace Sheenam.Api.Services.Foundations.Guests;

public partial class GuestService : IGuestService
{
    private readonly IStorageBroker storageBroker;
    private readonly ILoggingBroker loggingBroker;

    public GuestService(IStorageBroker storageBroker,
        ILoggingBroker loggingBroker)
    {
        this.storageBroker = storageBroker;
        this.loggingBroker = loggingBroker;
    }

    private string CreatePasswordHash(string password)
    {
        byte[] passwordHash;

        using (var hmacsha = SHA256.Create())
        {
            passwordHash = hmacsha.ComputeHash(Encoding.Default.GetBytes(password));
        };

        return Convert.ToBase64String(passwordHash);
    }

    public ValueTask<Guest> AddGuestAsync(Guest guest) =>
        TryCatch(async () =>
        {
            ValidateGuestOnAdd(guest);

            guest.Password = CreatePasswordHash(guest.Password);

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

    public async ValueTask<Guest> ModifyGuestAsync(Guest guest)
    {
        return await this.storageBroker.UpdateGuestAsync(guest);
    }
}
