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
        public async Task ShouldThrowValidationExceptionOnAddIfGuestIsNullAndLogItAsync()
        {
            // given
            Guest nullGuest = null;
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfGuestIsInvalidDataAndLogItAsync(string invalidData)
        {
            // given
            var invalidGuest = new Guest()
            {
                FirstName = invalidData
            };

            InvalidGuestException invalidGuestException = new();

            invalidGuestException.AddData(key: nameof(Guest.Id), 
                values: "Id is required");

            invalidGuestException.AddData(key: nameof(Guest.FirstName),
                values: "Text is invalid");

            invalidGuestException.AddData(key: nameof(Guest.LastName),
               values: "Text is invalid");

            invalidGuestException.AddData(key: nameof(Guest.DateOfBirth),
              values: "Date is invalid");

            invalidGuestException.AddData(key: nameof(Guest.Email),
               values: "Text is invalid");

            invalidGuestException.AddData(key: nameof(Guest.Address),
                values: "Text is invalid");

            var expectedGuestValidationExpected = 
                new GuestValidationException(invalidGuestException);

            // when
            ValueTask<Guest> addGuestTask =
               this.guestService.AddGuestAsync(invalidGuest);

            // then
            await Assert.ThrowsAsync<GuestValidationException>(() => addGuestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
              broker.LogError(It.Is(SameExceptionAs(expectedGuestValidationExpected))),
              Times.Once());

            this.storageBrokerMock.Verify(broker =>
              broker.InsertGuestAsync(It.IsAny<Guest>()), 
              Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowExceptionOnAddIfGenderIsInvalidAndLogItAsync()
        {
            // given
            Guest randomGuest = CreateRandomGuest();
            Guest invalidGuest = randomGuest;

            invalidGuest.Gender = GetInvalidEnum<GenderType>();
            var invalidGuestException = new InvalidGuestException();

            invalidGuestException.AddData(key: nameof(Guest.Gender), 
                values: "Value is invalid");

            var expectedGuestValidationException = 
                new GuestValidationException(invalidGuestException);

            // when
            ValueTask<Guest> AddGuestTask = 
                this.guestService.AddGuestAsync(invalidGuest);

            // then
            await Assert.ThrowsAsync<GuestValidationException>(() => 
               AddGuestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
               broker.LogError(It.Is(SameExceptionAs(expectedGuestValidationException))),
               Times.Once());

            this.storageBrokerMock.Verify(broker =>
               broker.InsertGuestAsync(It.IsAny<Guest>()),
               Times.Never());

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
