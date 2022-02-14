using RecordShopClassLibrary.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Create
{
    public class OrderCreateModel
    {
        public OrderCreateModel()
        {


        }
        public OrderCreateModel(Guid customerId, string customerName, decimal totalPrice, int address)
        {
       
            CustomerId = customerId;
            CustomerName = customerName;
            
            TotalPrice = totalPrice;
            AddressId = address;
        }

        
   
        public Guid CustomerId { get; set; }

        public string CustomerName { get; set; }

        public decimal TotalPrice { get; set; }

        public int AddressId { get; set; }

        public Guid OrderId { get; set; }

    }
}
