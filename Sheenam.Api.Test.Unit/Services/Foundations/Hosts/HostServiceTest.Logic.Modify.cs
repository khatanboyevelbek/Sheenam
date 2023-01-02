// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Host = Sheenam.Api.Models.Foundations.Hosts.Host;
using Xunit;
using Force.DeepCloner;
using FluentAssertions;
using Moq;

namespace Sheenam.Api.Test.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTest
    {
        [Fact]
        public async Task ShouldModifyHostAsync()
        {
            // when
            Host randomHost = CreateRandomHost();
            Host inputHost = randomHost;
            Host updatedGuest = inputHost.DeepClone();
            Host expectedGuest = inputHost.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateHostAsync(inputHost))
                .ReturnsAsync(updatedGuest);

            // when 
            Host actualHost =
               await this.hostservice.ModifyHostAsync(inputHost);

            // then
            actualHost.Should().BeEquivalentTo(expectedGuest);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateHostAsync(It.IsAny<Host>()), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
