using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Account
    {
        public int AccountId { get; set; }
        public int? ShelterId { get; set; }  // Nullable because not all accounts may be linked to a shelter
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? TokenExpireDate { get; set; }
        public DateTime? TokenCreateDate { get; set; }
        public string EmailConfirmToken { get; set; }
        public DateTime? EmailConfirmTokenExpire { get; set; }
        public bool isActivated { get; set; }

        // Navigation property
        public Shelter Shelter { get; set; }
        public UserProfile UserProfile { get; set; }
        public ICollection<AdoptionHistory> AdoptionHistories { get; set; }
        public ICollection<AdoptionApplication> AdoptionApplications { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Donation> Donations { get; set; }
        public ICollection<EventParticipation> Participations { get; set; }
        public ICollection<VolunteerActivity> VolunteerActivities { get; set; }
    }

}
