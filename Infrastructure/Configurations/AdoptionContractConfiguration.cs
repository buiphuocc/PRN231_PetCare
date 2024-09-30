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
    public class AdoptionContractConfiguration : IEntityTypeConfiguration<AdoptionContract>
    {
        public void Configure(EntityTypeBuilder<AdoptionContract> builder)
        {
            builder.HasKey(ac => ac.ContractId);

            // Relationship with AdoptionApplication
            builder.HasOne(ac => ac.Application)
                   .WithOne(aa => aa.Contract)
                   .HasForeignKey<AdoptionContract>(ac => ac.ApplicationId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(ac => ac.ContractFile).IsRequired(false);
            builder.Property(ac => ac.ContractText).IsRequired(false);
        }
    }
}
