using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Entities
{
     public class OrderEntity
    {
        public OrderEntity()
        {

        }

        public OrderEntity(Guid id, Guid customerId, string customerName, DateTime created, decimal totalPrice, int addressId)
        {
            Id = id;
            CustomerId = customerId;
            CustomerName = customerName;
            Created = created;
            TotalPrice = totalPrice;
            AddressId = addressId;
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public int AddressId { get; set; }
        
        public AddressEntity Address { get; set; }

        public virtual ICollection<OrderDetailEntity> OrderDetails { get; set; }
    }
}
