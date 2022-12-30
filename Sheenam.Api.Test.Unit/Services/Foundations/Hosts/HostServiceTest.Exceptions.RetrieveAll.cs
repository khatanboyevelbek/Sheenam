// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Host = Sheenam.Api.Models.Foundations.Hosts.Host;
using Xunit;
using Microsoft.Data.SqlClient;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;
using Moq;

namespace Sheenam.Api.Test.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTest
    {
        [Fact]
        public void ShouldThrowExceptionOnRetrieveAllIfSqlErrorOcurred()
        {
            // given
            SqlException sqlException = GetSqlError();

            var failedHostStorageException = 
                new FailedHostStorageException(sqlException);

            var hostDependencyException =
                new HostDependencyException(failedHostStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHosts()).Throws(sqlException);

            // when & then
            Assert.Throws<HostDependencyException>(() => 
                this.hostservice.RetrieveAllHosts());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHosts(), Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(hostDependencyException))),
                    Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowExceptionOnRetrieveAllIfServiceErrorOccured()
        {
            // given
            Exception exception = new Exception();

            var failedHostServiceException =
                new FailedHostServiceException(exception);

            var hostDependencyServiceException = 
                new HostDependencyServiceException(failedHostServiceException);

            this.storageBrokerMock.Setup(broker => 
                broker.SelectAllHosts()).Throws(exception);

            // when & then
            Assert.Throws<HostDependencyServiceException>(() => 
                this.hostservice.RetrieveAllHosts());

            this.storageBrokerMock.Verify(broker => 
                broker.SelectAllHosts(), Times.Once());

            this.loggingBrokerMock.Verify(broker => 
                broker.LogCritical(It.Is(SameExceptionAs(hostDependencyServiceException))),
                    Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
