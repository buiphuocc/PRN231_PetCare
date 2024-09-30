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
    public class CatProfileConfiguration : IEntityTypeConfiguration<CatProfile>
    {
        public void Configure(EntityTypeBuilder<CatProfile> builder)
        {
            builder.HasKey(cp => cp.ProfileId);

            // Relationship with Cat
            builder.HasOne(cp => cp.Cat)
                   .WithMany(c => c.CatProfiles)
                   .HasForeignKey(cp => cp.CatId)
                   .OnDelete(DeleteBehavior.Cascade); // If cat is deleted, delete profile
        }
    }
}
