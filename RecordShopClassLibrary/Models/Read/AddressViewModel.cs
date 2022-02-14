using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Read
{
    public class AddressViewModel
    {
        public AddressViewModel()
        {

        }
        public AddressViewModel(string streetAdress, string postalcode, string city)
        {

            StreetAdress = streetAdress;
            Postalcode = postalcode;
            City = city;
        }


        public string StreetAdress { get; set; }

        public string Postalcode { get; set; }

        public string City { get; set; }
    }
}
