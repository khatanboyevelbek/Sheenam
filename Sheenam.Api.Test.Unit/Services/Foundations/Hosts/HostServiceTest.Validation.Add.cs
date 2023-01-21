// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Moq;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Hosts;
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
            var nullHostException = new NullHostException();

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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowExceptionOnAddIfHostIsInvalidAndLogItAsync(string invalidData)
        {
            // given
            Host invalidHost = new Host()
            {
                LastName = invalidData
            };

            var invalidHostException = new InvalidHostException();
            invalidHostException.AddData(key: nameof(invalidHost.Id), values: "Id is required");
            invalidHostException.AddData(key: nameof(invalidHost.FirstName), values: "Text is required");
            invalidHostException.AddData(key: nameof(invalidHost.LastName), values: "Text is required");
            invalidHostException.AddData(key: nameof(invalidHost.DateOfBirth), values: "Date is required");
            invalidHostException.AddData(key: nameof(invalidHost.Email), values: "Text is required");
            invalidHostException.AddData(key: nameof(invalidHost.Password), values: "Text is required");
            invalidHostException.AddData(key: nameof(invalidHost.PhoneNumber), values: "Text is required");

            HostValidationException hostValidationException =
                new HostValidationException(invalidHostException);

            // when
            ValueTask<Host> AddHostTask =
                this.hostservice.AddHostAsync(invalidHost);

            // then
            await Assert.ThrowsAsync<HostValidationException>(() =>
                AddHostTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(hostValidationException))),
                    Times.Once());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHostAsync(It.IsAny<Host>()),
                    Times.Never());

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowExceptionOnAddIfGenderTypeIsInvalidAndLogItAsync()
        {
            // given
            Host randomHost = CreateRandomHost();
            randomHost.Gender = GetInvalidEnum<HostGenderType>();
            var invalidHostException = new InvalidHostException();

            invalidHostException.AddData(key: nameof(Host.Gender),
                values: "Value is invalid");

            var hostValidationException =
                new HostValidationException(invalidHostException);

            // when
            ValueTask<Host> AddHostTask = this.hostservice.AddHostAsync(randomHost);

            // then
            await Assert.ThrowsAsync<HostValidationException>(() =>
                AddHostTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
               broker.LogError(It.Is(SameExceptionAs(hostValidationException))),
                   Times.Once());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHostAsync(It.IsAny<Host>()),
                    Times.Never());

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
