using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Read
{
    public class OrderDetailProductViewModel
    {
        public Guid OrderId { get; set; }

        public int Quantity { get; set; }

        public ProductViewModel Product { get; set; }

        public decimal Price { get; set; }
    }
}
