// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Test.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTest
    {
        [Fact]
        public async Task ShouldAddHostAsync()
        {
            // given
            Host randomHost = CreateRandomHost();
            Host inputHost = randomHost;
            Host returnedHost = inputHost;
            Host expectedHost = returnedHost.DeepClone();

            this.storageBrokerMock.Setup(broker =>
               broker.InsertHostAsync(inputHost))
                 .ReturnsAsync(expectedHost);

            // when
            Host actualTask = await this.hostservice.AddHostAsync(inputHost);

            // then
            actualTask.Should().BeEquivalentTo(expectedHost);

            this.storageBrokerMock.Verify(broker =>
               broker.InsertHostAsync(inputHost),
               Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
