using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class EntityImageConfiguration : IEntityTypeConfiguration<EntityImage>
    {
        public void Configure(EntityTypeBuilder<EntityImage> builder)
        {
            // Define table name
            builder.ToTable("EntityImages");

            // Set up the primary key for the table
            builder.HasKey(ei => ei.ImageId);

            // Configure ImageId property as an auto-increment field
            builder.Property(ei => ei.ImageId)
                .IsRequired()
                .ValueGeneratedOnAdd();

            // Configure EntityId property
            builder.Property(ei => ei.EntityId)
                .IsRequired();

            // Configure EntityType property
            builder.Property(ei => ei.EntityType)
                .IsRequired()
                .HasMaxLength(200);

            // Configure ImageUrl property
            builder.Property(ei => ei.ImageUrl)
                .IsRequired()
                .HasMaxLength(255); // Adjust as needed

            // Configure UploadAt property with a default value
            builder.Property(ei => ei.UploadAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            // Configure IsPrimary property with a default value of false
            builder.Property(ei => ei.IsPrimary)
                .IsRequired()
                .HasDefaultValue(false);

            // Optional: Add an index for faster lookups on EntityId and EntityType
            builder.HasIndex(ei => new { ei.EntityId, ei.EntityType });
        }
    }
}
