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
    public class CatConfiguration : IEntityTypeConfiguration<Cat>
    {
        public void Configure(EntityTypeBuilder<Cat> builder)
        {
            builder.HasKey(c => c.CatId);

            // Shelter relationship
            builder.HasOne(c => c.Shelter)
                   .WithMany(s => s.Cats)
                   .HasForeignKey(c => c.ShelterId)
                   .OnDelete(DeleteBehavior.Cascade); // If shelter is deleted, delete cats
        }
    }
}
