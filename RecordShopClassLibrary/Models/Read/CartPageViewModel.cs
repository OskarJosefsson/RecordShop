using RecordShopClassLibrary.Models.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Read
{
    public class CartPageViewModel
    {
        public decimal TotalPrice { get; set; }
        public List<CartItemCreateModel> CartItems { get; set; }
    }
}
