// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Moq;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Services.Foundations.Guests;
using Tynamix.ObjectFiller;

namespace Sheenam.Api.Test.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        private readonly Mock<IStorageBroker> _storageBrokerMock;
        private readonly Mock<ILoggingBroker> _loggingBrokerMock;
        private readonly IGuestService _guestService;

        public GuestServiceTests()
        {
            this._storageBrokerMock = new Mock<IStorageBroker>();
            this._loggingBrokerMock = new Mock<ILoggingBroker>();

            this._guestService = 
                new GuestService(storageBroker: this._storageBrokerMock.Object, 
                loggingBroker: this._loggingBrokerMock.Object);
        }

        private static Guest? CreateRandomGuest() =>
            CreateGuestFiller(date: GetRandomDateTimeOffset).Create();

        private static DateTimeOffset? GetRandomDateTimeOffset =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<Guest> CreateGuestFiller(DateTimeOffset? date)
        {
            var filler = new Filler<Guest>();
            filler.Setup()
                .OnType<DateTimeOffset?>().Use(date);

            return filler;
        }
    }
}
