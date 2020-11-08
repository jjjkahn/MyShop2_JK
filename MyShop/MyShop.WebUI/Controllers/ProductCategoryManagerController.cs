using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        InMemoryRepository<ProductCategory>  context;//this comes form the DataAccess.InMemory is the CRUD operations in cache
        public ProductCategoryManagerController()
        {
            context = new InMemoryRepository<ProductCategory>();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> ProductCategories = context.Collection().ToList();
            return View(ProductCategories);
        }


        public ActionResult Create()
        {
            ProductCategory ProductCategory = new ProductCategory();
            return View(ProductCategory);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory ProductCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(ProductCategory);
            }
            else
            {
                context.Insert(ProductCategory);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);
            if (productCategoryToEdit != null)
            {

                return View(productCategoryToEdit);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, String Id)
        {
            ProductCategory productToEdit = context.Find(Id);
            if (productToEdit != null)
            {
                if (ModelState.IsValid)
                {
                    productToEdit.Category = productCategory.Category;
            

                    context.Commit();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(productCategory);
                }

            }
            else
            {
                return HttpNotFound();
            }
        }


        public ActionResult Delete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete != null)
            {

                return View(productCategoryToDelete);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete != null)
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}