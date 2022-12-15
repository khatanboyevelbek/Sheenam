// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using System.Runtime.InteropServices;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Guests.Exceptions;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.Guests
{
    public partial class GuestService
    {
        private delegate ValueTask<Guest> ReturningGuestFunction();

        private async ValueTask<Guest> TryCatch(ReturningGuestFunction returningGuestFunction)
        {
            try
            {
                return await returningGuestFunction();
            }
            catch (NullGuestException nullGuestException)
            {
                throw CreateAndLogValidationException(nullGuestException);
            }
            catch(InvalidGuestException invalidGuestException)
            {
                throw CreateAndLogValidationException(invalidGuestException);
            }
            catch(SqlException sqlException)
            {
                var failedGuestStorageException = new FailedGuestStorageException(sqlException);
                throw CreateAndLogCriticalException(failedGuestStorageException);
            }
            catch(DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistGuestException =
                    new AlreadyExistGuestException(duplicateKeyException);

                throw CreateAndLogDuplicateKeyException(alreadyExistGuestException);
            }
            catch(Exception exception)
            {
                var failedGuestServiceException = new FailedGuestServiceException(exception);
                throw CreateAndLogGuestDependencyServiceException(failedGuestServiceException);
            }
        }
        private GuestValidationException CreateAndLogValidationException(Xeption exception)
        {
            var guestValidationException = new GuestValidationException(exception);
            this.loggingBroker.LogError(guestValidationException);
            return guestValidationException;
        }

        private GuestDependencyException CreateAndLogCriticalException(Xeption exception)
        {
            var guestDependencyException = new GuestDependencyException(exception);
            this.loggingBroker.LogCritical(guestDependencyException);
            return guestDependencyException;
        }

        private GuestDependencyValidationException CreateAndLogDuplicateKeyException(Xeption exception)
        {
            var guestDependencyValidationException = new GuestDependencyValidationException(exception);
            this.loggingBroker.LogError(guestDependencyValidationException);
            return guestDependencyValidationException;
        }

        private GuestDependencyServiceException CreateAndLogGuestDependencyServiceException(Xeption exception)
        {
            var guestDependencyServiceException = new GuestDependencyServiceException(exception);
            this.loggingBroker.LogError(guestDependencyServiceException);
            return guestDependencyServiceException;
        }
    }
}
