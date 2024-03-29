﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;


namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
       IRepository<Product> context;
       IRepository<ProductCategory> productCategories;

        public ProductManagerController(IRepository<Product> Productcontext, IRepository<ProductCategory> ProductCategoryContext)
        {
            context = Productcontext;
            productCategories = ProductCategoryContext;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        //Form to get info
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }

        //Post info
        [HttpPost]
        public ActionResult Create(Product product)
        {
            //Check Any validations made in the model
            if(!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }


        }

        public ActionResult Edit(string id)
        {
            Product product = context.Find(id);

            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();
                return View(viewModel);
                
            }

        }

        [HttpPost]
        public ActionResult Edit(Product product,string id)
        {
            Product ProductToEdit = context.Find(id);

            if (ProductToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                else
                {


                    ProductToEdit.Category = product.Category;
                    ProductToEdit.Description = product.Description;
                    ProductToEdit.Image = product.Image;
                    ProductToEdit.Name = product.Name;
                    ProductToEdit.Price = product.Price;

                    context.Commit();

                    return RedirectToAction("Index");
                }
            }

        }

        public ActionResult Delete(string id)
        {
            Product productToDelete = context.Find(id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            Product productToDelete = context.Find(id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(id);
                //context.Commit();

                return RedirectToAction("Index");
            }
        }
    }
}