using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels.OrderDTO
{
    public class ShowOrderSuccessEmailDTO
    {
        public int orderid { get; set; }
        public string username { get; set; }
        public DateTime paymentdate { get; set; }
        public List<OrderItemEmailDto> orderitems { get; set; }
        public double totalprice => orderitems?.Sum(item => item.totalprice) ?? 0;
    }
}
