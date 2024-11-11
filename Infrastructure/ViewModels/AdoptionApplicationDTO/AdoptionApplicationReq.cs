using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels.AdoptionApplicationDTO
{
	public class AdoptionApplicationReq
	{
		public int CatId { get; set; }
		public int AdopterId { get; set; }
		public int AdoptionFee { get; set; }
		public string ApplicationStatus { get; set; } = "Pending";
        public DateTime ApplicationDate { get; set; }
		public DateTime? AdoptionDate { get; set; }

	}
}
