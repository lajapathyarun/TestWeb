using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.DAL.DbContexts;
using TestWebApi.Models;
using TestWebApi.Repositories.Interfaces;

namespace TestWebApi.Repository.Classes
{
    public class ProductRepository : IProductRepository, IDisposable
    {
        private ProductsContext db = new ProductsContext();//Hello

        public IEnumerable<Product> GetAll()
        {
            return db.Products;
        }
        public Product GetById(int id)
        {
            return db.Products.FirstOrDefault(p => p.Id == id);
        }
        public void Add(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
