using System;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using SportStore.Domain.Entities;
using SportStore.Domain.Abstract;
using Moq;

namespace SportStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory: DefaultControllerFactory
    {
        private IKernel _ninjectKernel;

        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            Product[] _products = {
            new Product { Name = "Kayak", Category = "Watersports", Price = 275M, Description = "Very usefull thing", ProductID = 1},
            new Product { Name = "Lifejacket", Category = "Watersports", Price = 48.95M, Description = "Very usefull thing", ProductID = 2},
            new Product { Name = "Soccer ball", Category = "Soccer", Price = 19.50M, Description = "Very usefull thing", ProductID = 3},
            new Product { Name = "Corner flag", Category = "Soccer", Price = 34.95M, Description = "Very usefull thing", ProductID = 4}};

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(_products.ToList().AsQueryable());

            _ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }

    }
}