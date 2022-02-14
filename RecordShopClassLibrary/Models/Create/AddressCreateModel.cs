using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Create
{
    public class AddressCreateModel
    {

        public string StreetAddress { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public Guid OrderId { get; set; }
    }
}

