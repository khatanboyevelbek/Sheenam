// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using FluentAssertions;
using Moq;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Guests.Exceptions;
using Xunit;

namespace Sheenam.Api.Test.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldThrowExceptionOnRetrieveByIdIfGuestIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidGuestId = Guid.Empty;
            var invalidGuestException = new InvalidGuestException();
            invalidGuestException.AddData(key: nameof(Guest.Id), values: "Id is required");

            var guestValidationException =
                new GuestValidationException(invalidGuestException);

            // when
            ValueTask<Guest> RetrieveGuestByIdTask =
                this.guestService.RetrieveGuestByIdAsync(invalidGuestId);

            var actualGuestValidationException =
                await Assert.ThrowsAsync<GuestValidationException>(RetrieveGuestByIdTask.AsTask);

            // then 
            actualGuestValidationException.Should().BeEquivalentTo(guestValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(guestValidationException))),
                Times.Once());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectGuestByIdAsync(It.IsAny<Guid>()), Times.Never());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
