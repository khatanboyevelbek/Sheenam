using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.HomeRequests;
using Sheenam.Api.Models.Foundations.Homes;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<HomeRequest> HomeRequests { get; set; }

        public async ValueTask<HomeRequest> InsertHomeRequestAsync(HomeRequest homeRequest) =>
            await InsertAsync(homeRequest);

        public IQueryable<HomeRequest> SelectAllHomeRequests() =>
            SelectAll<HomeRequest>();

        public async ValueTask<HomeRequest> SelectHomeRequestByIdAsync(Guid id) =>
            await SelectAsync<HomeRequest>(id);

        public async ValueTask<HomeRequest> UpdateHomeRequestAsync(HomeRequest homeRequest) =>
            await UpdateAsync(homeRequest);

        public async ValueTask<HomeRequest> DeleteHomeRequestAsync(HomeRequest homeRequest) =>
            await DeleteAsync(homeRequest);
    }
}
