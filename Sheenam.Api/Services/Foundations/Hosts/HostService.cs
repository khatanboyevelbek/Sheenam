// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Services.Foundations.Hosts
{
    public partial class HostService : IHostService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public HostService(IStorageBroker storageBroker, 
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Host> AddHostAsync(Host host)
        {
            try
            {
                if(host is null)
                {
                    throw new NullHostException();
                }
                return await this.storageBroker.InsertHostAsync(host);
            }
            catch (NullHostException nullHostException)
            {
                var hostValidationException = 
                    new HostValidationException(nullHostException);

                this.loggingBroker.LogError(hostValidationException);
                throw hostValidationException;
            }
        }
           
    }
}
