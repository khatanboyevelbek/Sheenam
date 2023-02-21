using Sheenam.Api.Models.Foundations.HomeRequests;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<HomeRequest> InsertHomeRequestAsync(HomeRequest homeRequest);
        IQueryable<HomeRequest> SelectAllHomeRequests();
        ValueTask<HomeRequest> SelectHomeRequestByIdAsync(Guid id);
        ValueTask<HomeRequest> UpdateHomeRequestAsync(HomeRequest homeRequest);
        ValueTask<HomeRequest> DeleteHomeRequestAsync(HomeRequest homeRequest);
    }
}
