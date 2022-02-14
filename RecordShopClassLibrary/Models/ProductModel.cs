using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models
{
     public  class ProductModel
    {

        public ProductModel()
        {

        }

        public ProductModel(int id, string name, string description, decimal price, string categoryName)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            CategoryName = categoryName;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string CategoryName { get; set; }
    }
}
