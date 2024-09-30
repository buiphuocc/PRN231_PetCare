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
    public class DonationConfiguration : IEntityTypeConfiguration<Donation>
    {
        public void Configure(EntityTypeBuilder<Donation> builder)
        {
            builder.HasKey(d => d.DonationId);

            // Relationships with Donor (Account) and Shelter
            builder.HasOne(d => d.Donor)
                   .WithMany(a => a.Donations)
                   .HasForeignKey(d => d.DonorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Shelter)
                   .WithMany(s => s.Donations)
                   .HasForeignKey(d => d.ShelterId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
