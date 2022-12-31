// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using System.Linq.Expressions;
using System.Runtime.Serialization;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Services.Foundations.Hosts;
using Tynamix.ObjectFiller;
using Xeptions;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Test.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTest
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IHostService hostservice;

        public HostServiceTest()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.hostservice = new HostService(storageBroker: storageBrokerMock.Object,
                loggingBroker: loggingBrokerMock.Object);
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue().ToString();

        private static SqlException GetSqlError() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private static int CreateRandomNumber() =>
            new IntRange(min: 0, max: 9).GetValue();

        private static T GetInvalidEnum<T>()
        {
            int randomNumber = CreateRandomNumber();

            while (Enum.IsDefined(typeof(T), randomNumber) is true)
            {
                randomNumber = CreateRandomNumber();
            }

            return (T)(Object)randomNumber;
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static IQueryable<Host> CreateRandomHosts() =>
            CreateHostFiller(date: CreateRandomDateTimeOffset())
            .Create(count: CreateRandomNumber()).AsQueryable();

        private static Host CreateRandomHost() =>
            CreateHostFiller(date: CreateRandomDateTimeOffset()).Create();

        private static DateTimeOffset CreateRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<Host> CreateHostFiller(DateTimeOffset date)
        {
            var filler = new Filler<Host>();
            filler.Setup().OnType<DateTimeOffset>().Use(date);
            return filler;
        }
    }
}
