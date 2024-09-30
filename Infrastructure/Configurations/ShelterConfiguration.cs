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
    public class ShelterConfiguration : IEntityTypeConfiguration<Shelter>
    {
        public void Configure(EntityTypeBuilder<Shelter> builder)
        {
            builder.HasKey(s => s.ShelterId);

            // Relationship with Manager (Account)
            builder.HasOne(s => s.Manager)
                   .WithOne(a => a.Shelter)
                   .HasForeignKey<Shelter>(s => s.ManagerId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
