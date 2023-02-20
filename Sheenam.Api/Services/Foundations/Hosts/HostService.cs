// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
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

        public ValueTask<Host> AddHostAsync(Host host)
        {
            return TryCatch(async () =>
            {
                ValidationHostOnAdd(host);

                host.Password = GenerateHashPassword(host.Password);
                host.CreatedDate = DateTimeOffset.UtcNow;
                host.UpdatedDate = DateTimeOffset.UtcNow;

                return await this.storageBroker.InsertHostAsync(host);
            });
        }

        public IQueryable<Host> RetrieveAllHosts() =>
            TryCatch(() => this.storageBroker.SelectAllHosts());

        public ValueTask<Host> RetrieveHostByIdAsync(Guid id) =>
            TryCatch(async () =>
            {
                ValidateHostId(id);
                return await this.storageBroker.SelectHostByIdAsync(id);
            });

        public ValueTask<Host> ModifyHostAsync(Host host)
        {
            return TryCatch(async () =>
            {
                ValidationHostOnModify(host);

                host.Password = GenerateHashPassword(host.Password);
                host.UpdatedDate = DateTimeOffset.UtcNow;

                return await this.storageBroker.UpdateHostAsync(host);
            });
        }

        public ValueTask<Host> RemoveHostAsync(Guid id) =>
            TryCatch(async () =>
            {
                ValidateHostId(id);

                Host deletedHost =
                    await this.storageBroker.SelectHostByIdAsync(id);

                return await this.storageBroker.DeleteHostAsync(deletedHost);
            });
    }
}
