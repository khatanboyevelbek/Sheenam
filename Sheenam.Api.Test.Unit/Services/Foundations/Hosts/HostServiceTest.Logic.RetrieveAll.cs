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
        public void ShouldRetrieveAllHosts()
        {
            // given
            IQueryable<Host> randomHosts = CreateRandomHosts();
            IQueryable<Host> storageHosts = randomHosts;
            IQueryable<Host> expectedHosts = storageHosts.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHosts()).Returns(storageHosts);

            // when
            IQueryable<Host> actualHosts =
                this.hostservice.RetrieveAllHosts();

            actualHosts.Should().BeEquivalentTo(expectedHosts);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHosts(), Times.Once());

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
