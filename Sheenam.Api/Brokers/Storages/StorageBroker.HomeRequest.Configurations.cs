using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.HomeRequests;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        private static void AddHomeRequestConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HomeRequest>().
                HasOne(hr => hr.Guest)
                .WithMany(g => g.HomeRequests)
                .HasForeignKey(hr => hr.GuestId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<HomeRequest>().
               HasOne(hr => hr.Home)
               .WithMany(g => g.Requests)
               .HasForeignKey(hr => hr.HomeId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
