// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Guests.Exceptions;
using Xunit;

namespace Sheenam.Api.Test.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Guest someGuest = CreateRandomGuest();
            SqlException sqlException = GetSqlError();
            FailedGuestStorageException failedGuestStorageException = new(sqlException);

            GuestDependencyException expectedGuestDependencyException =
                new(failedGuestStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertGuestAsync(someGuest))
                .ThrowsAsync(sqlException);

            // when
            ValueTask<Guest> AddGuestTask =
                this.guestService.AddGuestAsync(someGuest);

            // then
            await Assert.ThrowsAsync<GuestDependencyException>(() => AddGuestTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuestAsync(someGuest),
                Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(expectedGuestDependencyException))),
                Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowExceptiononAddIfDuplicateKeyErrorOccurs()
        {
            // given
            Guest someGuest = CreateRandomGuest();
            string someString = GetRandomString();

            var duplicateKeyException = new DuplicateKeyException(someString);

            var alreadyExistGuestException =
                new AlreadyExistGuestException(duplicateKeyException);

            var guestDependencyValidationException =
                new GuestDependencyValidationException(alreadyExistGuestException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertGuestAsync(someGuest))
                .ThrowsAsync(duplicateKeyException);

            // when 
            ValueTask<Guest> AddGuestTask =
                this.guestService.AddGuestAsync(someGuest);

            // then
            await Assert.ThrowsAnyAsync<GuestDependencyValidationException>(() =>
               AddGuestTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuestAsync(someGuest),
                Times.Once());

            this.loggingBrokerMock.Verify(broker =>
               broker.LogError(It.Is(SameExceptionAs(guestDependencyValidationException))),
               Times.Once());

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowExceptionOnAddIfServiceErrorOccurs()
        {
            // given
            Guest someGuest = CreateRandomGuest();
            var exception = new Exception();

            var failedGuestServiceException =
                new FailedGuestServiceException(exception);

            var guestDependencyServiceException =
                new GuestDependencyServiceException(failedGuestServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertGuestAsync(someGuest))
                .ThrowsAsync(exception);

            // when
            ValueTask<Guest> AddGuestTask =
                this.guestService.AddGuestAsync(someGuest);

            // then
            await Assert.ThrowsAsync<GuestDependencyServiceException>(() =>
                AddGuestTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuestAsync(someGuest),
                Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(guestDependencyServiceException))),
                Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
