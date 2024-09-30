using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AdoptionContract
    {
        public int ContractId { get; set; }
        public int ApplicationId { get; set; }
        public string ContractFile { get; set; }
        public string ContractText { get; set; }
        public DateTime SignedDate { get; set; }
        public string Witness { get; set; }

        // Navigation property
        public AdoptionApplication Application { get; set; }
    }

}
