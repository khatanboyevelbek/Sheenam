// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Moq;
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
            NullGuestException nullGuestException = new();

            GuestValidationException expectedGuestValidationException = 
                new(nullGuestException);

            // when
            ValueTask<Guest> addGuestTask = 
                this.guestService.AddGuestAsync(nullGuest);

            // then
            await Assert.ThrowsAsync<GuestValidationException>(() => 
              addGuestTask.AsTask());

            this.loggingBrokerMock.Verify(broker => 
                broker.LogError(It.Is(SameExceptionAs(expectedGuestValidationException))),
                Times.Once());

            this.storageBrokerMock.Verify(broker =>
             broker.InsertGuestAsync(It.IsAny<Guest>()), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
