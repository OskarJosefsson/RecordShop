using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecordShopMvc.Models.Entities;

namespace RecordShopMvc.Data
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {


        public AuthDbContext()
        {

        }


        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }


    }
}
