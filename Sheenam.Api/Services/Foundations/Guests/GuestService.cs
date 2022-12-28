// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using System.Text;
using System.Security.Cryptography;
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

        using(var hmacsha = new HMACSHA512())
        {
            passwordHash = hmacsha.ComputeHash(Encoding.ASCII.GetBytes(password));
        };

        return Encoding.ASCII.GetString(passwordHash);
    }

    public ValueTask<Guest> AddGuestAsync(Guest guest) =>
        TryCatch(async () =>
        {
            ValidateGuestOnAdd(guest);

            guest.Password = CreatePasswordHash(guest.Password);

            return await this.storageBroker.InsertGuestAsync(guest);
        });

    public IQueryable<Guest> RetrieveAllGuests()
    {
        return this.storageBroker.SelectAllGuests();
    }
}
