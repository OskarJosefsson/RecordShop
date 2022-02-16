using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Read
{
    public class ProductViewModel
    {

        public ProductViewModel()
        {

        }

        public ProductViewModel(int id, string name, string description, decimal price, string categoryName)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            this.CategoryName = categoryName;
        
        }

        public ProductViewModel(int id, string name, string description, decimal price, string category, int amount)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            CategoryName = category;
            
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string CategoryName { get; set; }

   

        public int Quantity { get; set; }



    }
}
