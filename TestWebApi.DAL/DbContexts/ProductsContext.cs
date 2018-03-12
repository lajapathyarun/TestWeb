using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TestWebApi.Models;

namespace TestWebApi.DAL.DbContexts
{
// Test Comment
    public class ProductsContext : DbContext
    {
        public ProductsContext()
            : base("name=ProductsContext")
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
