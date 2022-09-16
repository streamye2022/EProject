using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.ProductServices.Models;

namespace Microsoft.Streamye.ProductServices.Context
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        //持有两个 dbset 
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }
    }
}