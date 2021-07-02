using Product.Management.Business.Repository.Abstract;
using Product.Management.Business.Repository.Concrete;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Product.Management.UI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<ISubcategoryRepository, SubcategoryRepository>();
            container.RegisterType<IErrorRepository, ErrorRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}