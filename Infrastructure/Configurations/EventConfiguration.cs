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
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.EventId);

            // Relationship with Shelter
            builder.HasOne(e => e.Shelter)
                   .WithMany(s => s.Events)
                   .HasForeignKey(e => e.ShelterId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
