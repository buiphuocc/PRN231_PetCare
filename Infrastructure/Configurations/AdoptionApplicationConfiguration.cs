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
    public class AdoptionApplicationConfiguration : IEntityTypeConfiguration<AdoptionApplication>
    {
        public void Configure(EntityTypeBuilder<AdoptionApplication> builder)
        {
            builder.HasKey(aa => aa.ApplicationId);

            // Relationships with Cat and Adopter (Account)
            builder.HasOne(aa => aa.Cat)
                   .WithMany(c => c.AdoptionApplications)
                   .HasForeignKey(aa => aa.CatId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(aa => aa.Adopter)
                   .WithMany(a => a.AdoptionApplications)
                   .HasForeignKey(aa => aa.AdopterId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Optional adoption date
            builder.Property(aa => aa.AdoptionDate).IsRequired(false);
        }
    }
}
