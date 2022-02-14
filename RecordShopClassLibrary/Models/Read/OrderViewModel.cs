using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Read
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        public Guid CustomerId {get; set;}

        public string CustomerName { get; set;}

        public DateTime Created { get; set; }

        public decimal TotalPrice { get; set; }

        public AddressViewModel Address { get; set; }

        public IEnumerable<OrderDetailProductViewModel> OrderDetails { get; set; }
    }
}
