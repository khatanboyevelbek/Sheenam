// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Host = Sheenam.Api.Models.Foundations.Hosts.Host;
using Xunit;
using Force.DeepCloner;
using Moq;
using FluentAssertions;

namespace Sheenam.Api.Test.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTest
    {
        [Fact]
        public async Task ShouldRetrieveHostByIdAsync()
        {
            // given
            Guid someHostId = Guid.NewGuid();
            Host randomHost = CreateRandomHost();
            Host storageHost = randomHost;
            Host expectedHost = storageHost.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHostByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(storageHost);
            
            // when
            Host actualHost = 
                await this.hostservice.RetrieveHostByIdAsync(someHostId);

            // then
            actualHost.Should().BeEquivalentTo(expectedHost);

            this.storageBrokerMock.Verify(broker => 
                broker.SelectHostByIdAsync(It.IsAny<Guid>()), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
