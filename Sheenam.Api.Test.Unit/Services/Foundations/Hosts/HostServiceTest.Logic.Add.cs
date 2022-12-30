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
        private string CreatePasswordHash(string password)
        {
            byte[] passwordHash;

            using (var hmacsha = SHA256.Create())
            {
                passwordHash = hmacsha.ComputeHash(Encoding.Default.GetBytes(password));
            };

            return Convert.ToBase64String(passwordHash);
        }

        [Fact]
        public async Task ShouldAddHostAsync()
        {
            // given
            Host randomHost = CreateRandomHost();
            Host inputHost = randomHost;
            Host returnedHost = inputHost;
            Host expectedHost = returnedHost.DeepClone();

            expectedHost.Password =
                CreatePasswordHash(expectedHost.Password);

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
