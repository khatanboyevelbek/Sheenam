// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using System.Security.Cryptography;
using System.Text;
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

            expectedHost.Password =
                GenerateHashPassword(expectedHost.Password);

            this.storageBrokerMock.Setup(broker =>
               broker.InsertHostAsync(inputHost))
                 .ReturnsAsync(expectedHost);

            // when
            Host actualHost = await this.hostservice.AddHostAsync(inputHost);

            // then
            actualHost.Should().BeEquivalentTo(expectedHost);

            this.storageBrokerMock.Verify(broker =>
               broker.InsertHostAsync(inputHost),
               Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
