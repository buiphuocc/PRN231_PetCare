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
    public class EventParticipationConfiguration : IEntityTypeConfiguration<EventParticipation>
    {
        public void Configure(EntityTypeBuilder<EventParticipation> builder)
        {
            builder.HasKey(ep => ep.ParticipationId);

            // Relationships with Event and User
            builder.HasOne(ep => ep.Event)
                   .WithMany(e => e.Participations)
                   .HasForeignKey(ep => ep.EventId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ep => ep.User)
                   .WithMany(u => u.Participations)
                   .HasForeignKey(ep => ep.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
