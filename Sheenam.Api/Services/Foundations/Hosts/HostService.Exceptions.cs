// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using System;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;
using Xeptions;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Services.Foundations.Hosts
{
    public delegate ValueTask<Host> ReturningHostFunction();
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
            catch(InvalidHostException invalidHostException)
            {
               throw CreateExceptionIfHostIsInvalid(invalidHostException);
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
    }
}
