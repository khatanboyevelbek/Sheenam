// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Host = Sheenam.Api.Models.Foundations.Hosts.Host;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;
using Xunit;
using FluentAssertions;
using Moq;

namespace Sheenam.Api.Test.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTest
    {
        [Fact]
        public async Task ShouldThrowvalidationExceptionOnRetrieveByIdIfHostIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidHostId = Guid.Empty;
            var invalidHostException = new InvalidHostException();
            invalidHostException.AddData(key: nameof(Host.Id), values: "Id is required");
            var hostValidationException = new HostValidationException(invalidHostException);

            // when
            ValueTask<Host> RetrieveHostByIdTask = 
                this.hostservice.RetrieveHostByIdAsync(invalidHostId);

            var actualHostValidationException =
                await Assert.ThrowsAsync<HostValidationException>(RetrieveHostByIdTask.AsTask);

            // then
            actualHostValidationException.Should().BeEquivalentTo(hostValidationException);

            this.loggingBrokerMock.Verify(broker => 
                broker.LogError(It.Is(SameExceptionAs(hostValidationException))), Times.Once());

            this.storageBrokerMock.Verify(broker => 
                broker.SelectHostByIdAsync(It.IsAny<Guid>()), Times.Never());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
