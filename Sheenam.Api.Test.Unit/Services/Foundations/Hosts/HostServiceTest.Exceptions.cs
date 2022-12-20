// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;
using Xunit;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Test.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTest
    {
        [Fact]
        public async Task ShouldThrowExceptionOnAddIfDuplicateKeyErrorOccuredAndLogItAsync()
        {
            // given
            Host someHost = CreateRandomHost();
            string someString = GetRandomString();
            DuplicateKeyException duplicateKeyException = new DuplicateKeyException(someString);
            var alreadyExistHostException = new AlreadyExistHostException(duplicateKeyException);

            var hostDependencyValidationException = 
                new HostDependencyValidationException(alreadyExistHostException);

            this.storageBrokerMock.Setup(broker => broker.InsertHostAsync(someHost))
                .ThrowsAsync(duplicateKeyException);

            // when
            ValueTask<Host> AddHostTask = this.hostservice.AddHostAsync(someHost);

            // then
            await Assert.ThrowsAsync<HostDependencyValidationException>(() => 
                AddHostTask.AsTask());

            this.storageBrokerMock.Verify(broker => 
                broker.InsertHostAsync(someHost),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(hostDependencyValidationException))),
                    Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async Task ShouldThrowExceptionOnAddIfSqlErrorOccuredAndLogItAsync()
        {
            // given
            Host someHost = CreateRandomHost();
            SqlException sqlException = GetSqlError();

            var failedHostStorageException = 
                new FailedHostStorageException(sqlException);

            var hostDependencyException = 
                new HostDependencyException(failedHostStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertHostAsync(someHost))
                .ThrowsAsync(sqlException);

            // when
            ValueTask<Host> AddHostTask = this.hostservice.AddHostAsync(someHost);

            // then
            await Assert.ThrowsAsync<HostDependencyException>(() => 
                AddHostTask.AsTask());

            this.storageBrokerMock.Verify(broker => 
                broker.InsertHostAsync(It.IsAny<Host>()), 
                    Times.Once());

            this.loggingBrokerMock.Verify(broker => 
                broker.LogCritical(It.Is(SameExceptionAs(hostDependencyException))), 
                    Times.Once());

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();

        }
    }
}
