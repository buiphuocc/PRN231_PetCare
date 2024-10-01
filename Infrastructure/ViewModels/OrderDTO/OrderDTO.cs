using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels.OrderDTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int userid { get; set; }
        public string username { get; set; }
        public DateTime? PaymentDate { get; set; }
        public byte Status { get; set; }
        public List<OrderDetailsResDTO> orderdetails { get; set; }
    }

    public class OrderDetailsResDTO
    {
        public int Id { get; set; }
        public int productid { get; set; }
        public int Quantity { get; set; }
    }
}
