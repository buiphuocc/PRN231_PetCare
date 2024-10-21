using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels.AdoptionContractDTO
{
    public class AdoptionContractRes
    {
        public int ContractId { get; set; }
        public int ApplicationId { get; set; }
        public string ContractFile { get; set; }
        public string ContractText { get; set; }
        public DateTime SignedDate { get; set; }
        public string Witness { get; set; }
    }
}
