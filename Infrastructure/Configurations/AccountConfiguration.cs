using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.AccountId);

            // Email unique constraint
            builder.HasIndex(a => a.Email).IsUnique();

            // Shelter relationship (optional)
            builder.HasOne(a => a.Shelter)
                   .WithMany(s => s.Volunteers)
                   .HasForeignKey(a => a.ShelterId)
                   .OnDelete(DeleteBehavior.SetNull); // If shelter is deleted, set null
        }
    }
}
