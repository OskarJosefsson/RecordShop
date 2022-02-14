using RecordShopClassLibrary.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Create
{
    public class OrderDetailCreateModel
    {
        public OrderDetailCreateModel()
        {

        }

        public int Quantity { get; set; }
        
        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public Guid OrderId { get; set; }
    }
}
