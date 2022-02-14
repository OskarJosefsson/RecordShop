using Microsoft.EntityFrameworkCore;
using RecordShopClassLibrary.Models.Entities;

namespace RecordShopApi.Data
{
    public class SqlContext : DbContext
    {

        public SqlContext()
        {

        }

        public SqlContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<OrderEntity> Orders { get; set; }
        public virtual DbSet<OrderDetailEntity> OrderDetails{ get; set; }
        public virtual DbSet<AddressEntity> Addresses { get; set; }
        public virtual DbSet<CategoryEntity> Categories { get; set; }
        public virtual DbSet<ProductEntity> Products { get; set; }
    }
}
