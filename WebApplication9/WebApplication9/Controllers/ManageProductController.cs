using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication9.Models;
using WebApplication9.Repository;

namespace WebApplication9.Controllers
{
    public class ManageProductController : Controller
    {
        private readonly IProduct _product;
        public ManageProductController(IProduct product)
        {
            _product = product;
        }

        // GET: ManageProduct/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ManageProduct/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Quantity,Color,Price,ProductCode")] ProductVm product)
        {
            if (ModelState.IsValid)
            {
              //  _product.InsertProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }


     //to do get list method
        // GET: ManageProduct/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var product = _product.GetProductByProductId(Convert.ToInt32(id));
            return View(product);
        }

        // GET: ManageProduct
        public IActionResult Index()
        {
            return View(_product.GetProducts());
        }

        // GET: ManageProduct/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var product = _product.GetProductByProductId(Convert.ToInt32(id));
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProductId,Name,Quantity,Color,Price,ProductCode")] Product product)
        {
            if (id != product.ExpenseId)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                try
                {
                   
                        _product.UpdateProduct(product);
                }
                catch (Exception ex)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: ManageProduct/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var product = _product.GetProductByProductId(Convert.ToInt32(id));

            if (product == null)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // POST: ManageProduct/Delete/5
        [HttpPost, ActionName("Delete")]

        public IActionResult DeleteConfirmed(int id)
        {
            _product.DeleteProduct(id);
            return RedirectToAction(nameof(Index));

        }




    }
}
