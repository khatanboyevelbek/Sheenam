using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.Homes;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Home> Homes { get; set; }

        public async ValueTask<Home> InsertHomeAsync(Home home) =>
            await InsertAsync(home);

        public IQueryable<Home> SelectAllHomes() =>
            SelectAll<Home>();

        public async ValueTask<Home> SelectHomeByIdAsync(Guid id) =>
            await SelectAsync<Home>(id);

        public async ValueTask<Home> UpdateHomeAsync(Home home) =>
            await UpdateAsync(home);

        public async ValueTask<Home> DeleteHomeAsync(Home home) =>
            await DeleteAsync(home);
    }
}
