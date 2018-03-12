using System.Web.Http;
using TestWebApi.Repositories.Interfaces;
using TestWebApi.Repository.Classes;
using Unity;
using Unity.WebApi;

namespace TestWebApi
{
    public class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IProductRepository, ProductRepository>();
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}