using RecordShopClassLibrary.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Read
{
    public class OrderDetailViewModel
    {
        public OrderDetailViewModel()
        {

        }
        public OrderDetailViewModel(int quantity, int productId, decimal price)
        {
            Quantity = quantity;
            ProductId = productId;
            Price = price;
        }

        public Guid OrderId { get; set; }

        public int Quantity { get; set; }
     
        public int ProductId { get; set; }

        public ProductViewModel Product { get; set; }

        public decimal Price { get; set; }

    }
}
