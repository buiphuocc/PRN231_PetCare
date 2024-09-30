using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{ 
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(up => up.UserProfileId);

            // Relationship with Account
            builder.HasOne(up => up.Account)
                   .WithOne(a => a.UserProfile)
                   .HasForeignKey<UserProfile>(up => up.AccountId)
                   .OnDelete(DeleteBehavior.Cascade); // If account is deleted, delete profile
        }
    }

}
