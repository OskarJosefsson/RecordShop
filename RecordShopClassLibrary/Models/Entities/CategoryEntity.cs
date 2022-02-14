using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Entities
{
    public class CategoryEntity
    {

        public CategoryEntity()
        {

        }

        public CategoryEntity(string name)
        {
            Name = name;
        }

        public CategoryEntity(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [Key]

        public int Id { get; set; }

        [Required]

        public string Name { get; set; }

        public virtual ICollection<ProductEntity> Products { get; set; }
    }
}
