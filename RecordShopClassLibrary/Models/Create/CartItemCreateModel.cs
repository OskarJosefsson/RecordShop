using RecordShopClassLibrary.Models.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Create
{
    public class CartItemCreateModel
    {
        public CartItemCreateModel()
        {

        }
        public CartItemCreateModel(ProductModel product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
    }
}
