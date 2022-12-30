// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;
using Xeptions;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Services.Foundations.Hosts
{
    public delegate ValueTask<Host> ReturningHostFunction();
    public delegate IQueryable<Host> ReturningHostsFunction();
    public partial class HostService
    {
        public async ValueTask<Host> TryCatch(ReturningHostFunction handler)
        {
            try
            {
                return await handler();
            }
            catch (NullHostException nullHostException)
            {
                throw CreateExceptionIfHostIsNull(nullHostException);
            }
            catch (InvalidHostException invalidHostException)
            {
                throw CreateExceptionIfHostIsInvalid(invalidHostException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistHostException =
                    new AlreadyExistHostException(duplicateKeyException);

                throw CreateExceptionIfDuplicateKeyErrorOccured(alreadyExistHostException);
            }
            catch (SqlException sqlException)
            {
                var failedHostStorageException =
                    new FailedHostStorageException(sqlException);

                throw CreateExceptionIfSqlErrorOccured(failedHostStorageException);
            }
            catch (Exception exception)
            {
                var failedHostServiceException =
                    new FailedHostServiceException(exception);

                throw CreateExceptionIfServiceErrorOccured(failedHostServiceException);
            }
        }

        public IQueryable<Host> TryCatch(ReturningHostsFunction returningHostsFunction)
        {
            try
            {
                return returningHostsFunction();
            }
            catch (SqlException sqlException)
            {
                var failedHostStorageException =
                    new FailedHostStorageException(sqlException);

                throw CreateExceptionIfSqlErrorOccured(failedHostStorageException);
            }
            catch (Exception exception)
            {
                var failedHostServiceException
                    = new FailedHostServiceException(exception);

                throw CreateExceptionIfServiceErrorOccured(failedHostServiceException);
            }
        }

        private HostValidationException CreateExceptionIfHostIsNull(Xeption innerException)
        {
            var hostValidationException =
                    new HostValidationException(innerException);

            this.loggingBroker.LogError(hostValidationException);
            return hostValidationException;
        }

        private HostValidationException CreateExceptionIfHostIsInvalid(Xeption innerException)
        {
            var hostValidationException =
                    new HostValidationException(innerException);

            this.loggingBroker.LogError(hostValidationException);
            return hostValidationException;
        }

        private HostDependencyValidationException CreateExceptionIfDuplicateKeyErrorOccured(Xeption innerException)
        {
            var hostDependencyValidationException =
                new HostDependencyValidationException(innerException);

            this.loggingBroker.LogError(hostDependencyValidationException);
            return hostDependencyValidationException;
        }

        private HostDependencyException CreateExceptionIfSqlErrorOccured(Xeption innerException)
        {
            var hostDependencyException =
                new HostDependencyException(innerException);

            this.loggingBroker.LogCritical(hostDependencyException);
            return hostDependencyException;
        }

        private HostDependencyServiceException CreateExceptionIfServiceErrorOccured(Xeption innerException)
        {
            var hostDependencyServiceException =
               new HostDependencyServiceException(innerException);

            this.loggingBroker.LogCritical(hostDependencyServiceException);
            return hostDependencyServiceException;
        }
    }
}
