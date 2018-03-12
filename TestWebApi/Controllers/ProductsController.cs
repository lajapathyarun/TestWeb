using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestWebApi.Models;
using TestWebApi.Repositories.Interfaces;
using TestWebApi.Repository;

namespace TestWebApi.Controllers
{
    public class ProductsController : ApiController
    {
        // This line of code is a problem!
        private IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Product> Get()
        {
            return _repository.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}