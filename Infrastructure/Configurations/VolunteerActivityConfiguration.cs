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
    public class VolunteerActivityConfiguration : IEntityTypeConfiguration<VolunteerActivity>
    {
        public void Configure(EntityTypeBuilder<VolunteerActivity> builder)
        {
            builder.HasKey(va => va.ActivityId);

            // Relationship with User and Shelter
            builder.HasOne(va => va.User)
                   .WithMany(u => u.VolunteerActivities)
                   .HasForeignKey(va => va.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(va => va.Shelter)
                   .WithMany(s => s.VolunteerActivities)
                   .HasForeignKey(va => va.ShelterId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
