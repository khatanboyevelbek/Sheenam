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
        public async Task ShouldRemoveHostAsync()
        {
            // given
            Guid randomHostId = Guid.NewGuid();
            Host randomHost = CreateRandomHost();
            Host storageHost = randomHost.DeepClone();
            Host deletedHost = randomHost.DeepClone();
            Host expectedDeletedHost = deletedHost.DeepClone();

            this.storageBrokerMock.Setup(broker => 
                broker.SelectHostByIdAsync(It.IsAny<Guid>())).ReturnsAsync(storageHost);

            this.storageBrokerMock.Setup(broker => 
                broker.DeleteHostAsync(storageHost)).ReturnsAsync(deletedHost);

            // when
            var actualDeletedHost = 
                await this.hostservice.RemoveHostAsync(randomHostId);

            // then
            actualDeletedHost.Should().BeEquivalentTo(expectedDeletedHost);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHostByIdAsync(It.IsAny<Guid>()), Times.Once());

            this.storageBrokerMock.Verify(broker =>
               broker.DeleteHostAsync(It.IsAny<Host>()), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();

        }

    }
}
