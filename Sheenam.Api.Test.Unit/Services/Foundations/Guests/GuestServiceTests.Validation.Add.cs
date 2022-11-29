// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Guests.Exceptions;
using Xunit;

namespace Sheenam.Api.Test.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfGuestIsNullAndLogItAsync()
        {
            // given
            Guest? nullGuest = null;
            var nullGuestException = new NullGuestException();

            var guestValidationException = 
                new GuestValidationException(nullGuestException);

            // when
            ValueTask<Guest> AddGuestTask() =>
                await this.guestService.AddGuestAsync(nullGuest);

            // then
            await Assert.ThrowsAsync<GuestValidationException>(() => AddGuestTask().AsTask());
        }
    }
}
