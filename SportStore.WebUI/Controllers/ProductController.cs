using SportStore.Domain.Abstract;
using SportStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;
        public const int PageSize = 4;

        public ProductController(IProductRepository repository)
        {
            this._repository = repository;
        }

        public ActionResult List(string category, int page = 1)
        {
            ProductListViewModel viewModel = new ProductListViewModel
            {
                Products = _repository.Products
                .Where(p => p.Category == null || p.Category == category || category == null)
                .OrderBy(p => p.Category)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                
                PagingInfo = new PagingInfo { 
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? _repository.Products.Count() : 
                    _repository.Products.Where(p => p.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(viewModel);
        }
    }
}
