using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class SpendingConfiguration : IEntityTypeConfiguration<Spending>
    {
        public void Configure(EntityTypeBuilder<Spending> builder)
        {
            builder.HasKey(s => s.SpendingId);

            // Relationship with Shelter
            builder.HasOne(s => s.Shelter)
                   .WithMany(sh => sh.Spendings)
                   .HasForeignKey(s => s.ShelterId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
