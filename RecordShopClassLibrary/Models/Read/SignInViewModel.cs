using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Read
{
    public class SignInViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]

        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]

        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
