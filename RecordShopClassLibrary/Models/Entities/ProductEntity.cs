using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Entities
{
    public class ProductEntity
    {
        public ProductEntity()
        {

        }

        public ProductEntity(string name, string description, decimal price, int categoryID)
        {
            Name = name;
            Description = description;
            Price = price;
            CategoryID = categoryID;
        }

        public ProductEntity(int id, string name, string description, decimal price, int categoryID)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            CategoryID = categoryID;
        }
        [Key]

        public int Id { get; set; }

        [Required]

        public string Name { get; set; }

        [Required]

        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public int CategoryID { get; set; }

        public CategoryEntity Category { get; set; }
    }
}
