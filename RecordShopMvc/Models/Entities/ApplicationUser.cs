using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RecordShopMvc.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        [PersonalData]
        public string FirstName { get; set; }
        [Required]
        [PersonalData]
        public string LastName { get; set; }

    }
}
