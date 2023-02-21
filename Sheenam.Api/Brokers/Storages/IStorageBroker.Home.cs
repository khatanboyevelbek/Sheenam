using Sheenam.Api.Models.Foundations.Homes;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Home> InsertHomeAsync(Home home);
        IQueryable<Home> SelectAllHomes();
        ValueTask<Home> SelectHomeByIdAsync(Guid id);
        ValueTask<Home> UpdateHomeAsync(Home home);
        ValueTask<Home> DeleteHomeAsync(Home home);
    }
}
