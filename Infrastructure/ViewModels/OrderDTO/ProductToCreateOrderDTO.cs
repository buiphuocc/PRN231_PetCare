using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels.OrderDTO
{
    public class ProductToCreateOrderDTO
    {
        public int productid { get; set; }
        public string nameproduct { get; set; }
        public string descriptionproduct { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        //public int CategoryId { get; set; }
        public string namecategory { get; set; }
        //public int MaterialId { get; set; }
        public string namematerial { get; set; }
        //public int GenderId { get; set; }
        public string imageurl { get; set; }
        public int orderid { get; set; }
    }
}
