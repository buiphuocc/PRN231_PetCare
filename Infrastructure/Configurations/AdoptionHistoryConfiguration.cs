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
    public class AdoptionHistoryConfiguration : IEntityTypeConfiguration<AdoptionHistory>
    {
        public void Configure(EntityTypeBuilder<AdoptionHistory> builder)
        {
            builder.HasKey(ah => ah.AdoptionId);

            // Relationships with Cat and Account
            builder.HasOne(ah => ah.Cat)
                   .WithMany(c => c.AdoptionHistories)
                   .HasForeignKey(ah => ah.CatId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ah => ah.Adopter)
                   .WithMany(a => a.AdoptionHistories)
                   .HasForeignKey(ah => ah.AdopterId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
