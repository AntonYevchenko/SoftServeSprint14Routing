using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsWithRouting.Models;
using ProductsWithRouting.Services;

namespace ProductsWithRouting.Controllers
{
    public class ProductsController:Controller
    {
        private List<Product> myProducts;

        private readonly FilterProductsService _filterProductService;

        public ProductsController(Data data, FilterProductsService filterProductService)
        {
            myProducts = data.Products;
            _filterProductService = filterProductService;
        }

        public IActionResult Index(int? filterId, string filterName)
        {
            var filteredProducts = _filterProductService.FilterProducts(myProducts, filterId, filterName);

            return View(filteredProducts);
        }

        public IActionResult View(int? id)
        {
            if (id is null)
            {
                return View(new Product());
            }
            if (!myProducts.Any(p => p.Id == id))
            {
                Response.StatusCode = 404;
                return View("ProductNotFound");
            }
            var product = myProducts.Find(product => product.Id == id);
            return View(product);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!myProducts.Any(p => p.Id == id))
            {
                Response.StatusCode = 404;
                return View("ProductNotFound");
            }
            //Please, add your implementation of the method
            return View(/*TODO: pass corresponding product here*/);
        }
        [HttpPost]
        public IActionResult Edit(Product editedProduct)
        {
            var existingProduct = myProducts.Find(product => product.Id == editedProduct.Id);
            existingProduct.Name = editedProduct.Name;
            existingProduct.Description = editedProduct.Description;

            TempData["SuccessMessage"] = "Changes saved successfully!";

            return View(existingProduct);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                myProducts.Add(product);
                return RedirectToAction("Index");
            }
            return Create(product);

        }

        public IActionResult Create()
        {
            return View(new Product() { Id = myProducts.MaxBy(product => product.Id).Id + 1 });
        }

        public IActionResult Delete(int id)
        {
            if (!myProducts.Any(p => p.Id == id))
            {
                Response.StatusCode = 404;
                return View("ProductNotFound");
            }
            var deletedProduct = myProducts.Find(product => product.Id == id);
            myProducts.Remove(deletedProduct);
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
