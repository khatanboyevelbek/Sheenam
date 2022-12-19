// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Moq;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;
using Xunit;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Test.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTest
    {
        [Fact]
        public async Task ShouldThowExceptionOnAddIfHostIsNullAndLogItAsync()
        {
            // given
            Host nullHost = null;
            var nullHostException= new NullHostException();

            HostValidationException hostValidationException = 
                new HostValidationException(nullHostException);

            // when 
            ValueTask<Host> AddHostTask =
                this.hostservice.AddHostAsync(nullHost);

            // then
            await Assert.ThrowsAsync<HostValidationException>(() => 
                AddHostTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(hostValidationException))),
                Times.Once());

            this.storageBrokerMock.Verify(broker => 
                broker.InsertHostAsync(It.IsAny<Host>()), 
                Times.Never());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
