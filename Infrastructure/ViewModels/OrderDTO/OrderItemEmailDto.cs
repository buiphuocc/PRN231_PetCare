using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels.OrderDTO
{
    public class OrderItemEmailDto
    {
        public string productname { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double totalprice => Quantity * Price;
    }
}
