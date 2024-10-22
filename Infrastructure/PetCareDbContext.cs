using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public class PetCareDbContext : DbContext
    {
        public PetCareDbContext(DbContextOptions<PetCareDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Cat> Cats { get; set; }
        public DbSet<CatProfile> CatProfiles { get; set; }
        public DbSet<AdoptionHistory> AdoptionHistories { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AdoptionApplication> AdoptionApplications { get; set; }
        public DbSet<AdoptionContract> AdoptionContracts { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Spending> Spendings { get; set; }
        public DbSet<VolunteerActivity> VolunteerActivities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipation> EventParticipations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
            modelBuilder.ApplyConfiguration(new CatConfiguration());
            modelBuilder.ApplyConfiguration(new CatProfileConfiguration());
            modelBuilder.ApplyConfiguration(new AdoptionHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new ShelterConfiguration());
            modelBuilder.ApplyConfiguration(new AdoptionApplicationConfiguration());
            modelBuilder.ApplyConfiguration(new AdoptionContractConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new DonationConfiguration());
            modelBuilder.ApplyConfiguration(new SpendingConfiguration());
            modelBuilder.ApplyConfiguration(new VolunteerActivityConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new EventParticipationConfiguration());
        }
    }
}
