using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Models;

namespace TestWebApi.Repositories.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Add(Product product);
    }
}
