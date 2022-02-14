using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Create
{
    public class SignUpViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]

        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]

        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password did not match")]

        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Firstname")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Lastsname")]
        public string LastName { get; set; }
    }
}
