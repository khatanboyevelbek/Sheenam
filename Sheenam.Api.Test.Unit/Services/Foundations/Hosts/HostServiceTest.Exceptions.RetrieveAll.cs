// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Host = Sheenam.Api.Models.Foundations.Hosts.Host;
using Xunit;
using Microsoft.Data.SqlClient;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;

namespace Sheenam.Api.Test.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTest
    {
        [Fact]
        public void ShouldThrowExceptionOnRetrieveAllIfSqlErrorOcurred()
        {
            // given
            SqlException sqlException = GetSqlError();

            var failedHostStorageException = 
                new FailedHostStorageException(sqlException);

            var hostDependencyException =
                new HostDependencyException(failedHostStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHosts()).Throws(sqlException);

            // when & then
            Assert.Throws<HostDependencyException>(() => 
                this.hostservice.RetrieveAllHosts());
        }
    }
}
