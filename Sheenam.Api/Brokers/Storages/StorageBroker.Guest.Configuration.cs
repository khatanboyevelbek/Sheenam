// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using EFxceptions;
using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.Guests;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker : EFxceptionsContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guest>()
                .Property(guest => guest.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Guest>()
               .Property(guest => guest.LastName)
               .IsRequired()
               .HasMaxLength(100);

            modelBuilder.Entity<Guest>()
               .Property(guest => guest.DateOfBirth)
               .IsRequired();

            modelBuilder.Entity<Guest>()
               .Property(guest => guest.Email)
               .IsRequired();

            modelBuilder.Entity<Guest>()
               .Property(guest => guest.PhoneNumber)
               .HasMaxLength(50);

            modelBuilder.Entity<Guest>()
                .Property(guest => guest.Address)
                .IsRequired()
                .HasColumnType("ntext");
        }
    }
}
