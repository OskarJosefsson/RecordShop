using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Entities
{
    public class AddressEntity
    {
        public AddressEntity()
        {

        }

        public AddressEntity(string streetAdress, string postalcode, string city)
        {
            StreetAdress = streetAdress;
            Postalcode = postalcode;
            City = city;
        }

        public AddressEntity(int id, string streetAdress, string postalcode, string city)
        {
            Id = id;
            StreetAdress = streetAdress;
            Postalcode = postalcode;
            City = city;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string StreetAdress { get; set; }
        [Required]
        public string Postalcode { get; set; }
        [Required]
        public string City { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; }


    }
}
