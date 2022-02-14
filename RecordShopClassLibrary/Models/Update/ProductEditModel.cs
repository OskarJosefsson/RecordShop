using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Update
{
    public class ProductEditModel
    {

        public ProductEditModel()
        {

        }
        public ProductEditModel(string name, string description, decimal price, int categoryID)
        {

            Name = name;
            Description = description;
            Price = price;
            CategoryID = categoryID;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryID { get; set; }
    }
}
