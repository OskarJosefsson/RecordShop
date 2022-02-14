using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Create
{
    public class OrderAddressCreateModel
    {
        public AddressCreateModel Address { get; set; }

        public OrderCreateModel Order { get; set; }

        public IEnumerable<CartItemCreateModel> CartItems { get; set; }
    }
}
