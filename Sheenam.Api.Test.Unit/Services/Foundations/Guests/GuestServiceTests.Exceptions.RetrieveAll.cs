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
        public void ShouldThrowExceptionOnRetrieveAllIfSqlErrorOccured()
        {
            // given
            SqlException sqlException = GetSqlError();

            var failedGuestStorageException =
                new FailedGuestStorageException(sqlException);

            var guestDependencyException =
                new GuestDependencyException(failedGuestStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllGuests()).Throws(sqlException);

            // when 
            Action retrieveAllGuestsAction = () => this.guestService.RetrieveAllGuests();

            // then
            Assert.Throws<GuestDependencyException>(retrieveAllGuestsAction);

            this.storageBrokerMock.Verify(broker => 
                broker.SelectAllGuests(), Times.Once());

            this.loggingBrokerMock.Verify(broker => 
                broker.LogCritical(It.Is(SameExceptionAs(guestDependencyException))), 
                    Times.Once());

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThowExceptionOnRetrieveAllIfServiceErrorOccured()
        {
            // given
            var exception = new Exception();

            var failedGuestServiceException = 
                new FailedGuestServiceException(exception);

            GuestDependencyServiceException guestDependencyServiceException = 
                new GuestDependencyServiceException(failedGuestServiceException);

            this.storageBrokerMock.Setup(broker => 
                broker.SelectAllGuests()).Throws(exception);

            // when & then
            Assert.Throws<GuestDependencyServiceException>(() => 
                this.guestService.RetrieveAllGuests());

            this.storageBrokerMock.Verify(broker => 
                broker.SelectAllGuests(), Times.Once());

            this.loggingBrokerMock.Verify(broker => 
                broker.LogCritical(It.Is(SameExceptionAs(guestDependencyServiceException))), 
                    Times.Once());

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
