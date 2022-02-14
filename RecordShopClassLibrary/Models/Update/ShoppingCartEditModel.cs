using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Update
{
    public class ShoppingCartEditModel
    {

        public ShoppingCartEditModel()
        {

        }
        public ShoppingCartEditModel(Guid id, int cartItemId)
        {
            Id = id;
            CartItemId = cartItemId;
        }

        public Guid Id { get; set; }

        public int CartItemId { get; set; }
    }
}
