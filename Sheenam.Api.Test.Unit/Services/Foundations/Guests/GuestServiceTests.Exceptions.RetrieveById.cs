// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

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
        public async Task ShouldThrowExceptionOnRetrieveByIdIfSqlErrorOccurred()
        {
            // given
            Guid randomId = Guid.NewGuid();
            SqlException sqlException = GetSqlError();

            var failedGuestStorageException = 
                new FailedGuestStorageException(sqlException);

            var guestDependencyException =
                new GuestDependencyException(failedGuestStorageException);

            this.storageBrokerMock.Setup(broker => 
                broker.SelectGuestByIdAsync(randomId)).ThrowsAsync(sqlException);

            // when
            ValueTask<Guest> RetrieveByIdTask = 
                this.guestService.RetrieveGuestById(randomId);

            // then
            await Assert.ThrowsAsync<GuestDependencyException>(() => 
                RetrieveByIdTask.AsTask());

            this.storageBrokerMock.Verify(broker => 
                broker.SelectGuestByIdAsync(It.IsAny<Guid>()), Times.Once());

            this.loggingBrokerMock.Verify(broker => 
            broker.LogCritical(guestDependencyException), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
