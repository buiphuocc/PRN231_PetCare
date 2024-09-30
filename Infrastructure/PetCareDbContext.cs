using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class PetCareDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public PetCareDbContext( IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Cat> Cats { get; set; }
        public DbSet<CatProfile> CatProfiles { get; set; }
        public DbSet<AdoptionHistory> AdoptionHistories { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<AdoptionApplication> AdoptionApplications { get; set; }
        public DbSet<AdoptionContract> AdoptionContracts { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Spending> Spendings { get; set; }
        public DbSet<VolunteerActivity> VolunteerActivities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipation> EventParticipations { get; set; }

        public PetCareDbContext(DbContextOptions<PetCareDbContext> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }
        private string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true).Build();
            return configuration["ConnectionStrings:Local"];
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<UserProfile>(new UserProfileConfiguration());
            modelBuilder.ApplyConfiguration<Cat>(new CatConfiguration());
            modelBuilder.ApplyConfiguration<CatProfile>(new CatProfileConfiguration());
            modelBuilder.ApplyConfiguration<AdoptionHistory>(new AdoptionHistoryConfiguration());
            modelBuilder.ApplyConfiguration<Shelter>(new ShelterConfiguration());
            modelBuilder.ApplyConfiguration<AdoptionApplication>(new AdoptionApplicationConfiguration());
            modelBuilder.ApplyConfiguration<AdoptionContract>(new AdoptionContractConfiguration());
            modelBuilder.ApplyConfiguration<Appointment>(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration<Donation>(new DonationConfiguration());
            modelBuilder.ApplyConfiguration<Spending>(new SpendingConfiguration());
            modelBuilder.ApplyConfiguration<VolunteerActivity>(new VolunteerActivityConfiguration());
            modelBuilder.ApplyConfiguration<Event>(new EventConfiguration());
            modelBuilder.ApplyConfiguration<EventParticipation>(new EventParticipationConfiguration());
        }
    }
}
