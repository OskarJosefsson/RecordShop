using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Entities
{
    public class OrderDetailEntity
    {
        public OrderDetailEntity()
        {

        }
        public OrderDetailEntity( int quantity, int productId, decimal price, Guid orderId)
        {
           
            Quantity = quantity;
            ProductId = productId;
            Price = price;
            OrderId = orderId;
            
        }

        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int ProductId { get; set; }
        
        public decimal Price { get; set; }  

        public ProductEntity Product { get; set; }

        public Guid OrderId { get; set; }

        public OrderEntity Order { get; set; }


    }
}
