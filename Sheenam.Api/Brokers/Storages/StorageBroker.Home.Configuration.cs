using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.Homes;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        private static void AddHomeStorageConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Home>()
               .HasOne(h => h.Host)
               .WithMany(hs => hs.Homes)
               .HasForeignKey(h => h.HostId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
