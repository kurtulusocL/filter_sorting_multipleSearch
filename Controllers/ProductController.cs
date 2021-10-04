using Filter_Search.DataAccess.Context;
using Filter_Search.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filter_Search.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext _db;
        public ProductController()
        {
            _db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View(_db.Products.Include("Category").Include("Country").OrderBy(i => i.Stock).ToList());
        }
        public ActionResult Create()
        {
            ViewBag.Categories = _db.Categories.Include("Products").OrderByDescending(i => i.Products.Count()).ToList();
            ViewBag.Countries = _db.Countries.Include("Products").OrderByDescending(i => i.Products.Count()).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(model);
                _db.Entry(model).State = EntityState.Added;
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Create));
        }
        public ActionResult Edit(int? id)
        {
            ViewBag.Categories = _db.Categories.Include("Products").OrderByDescending(i => i.Products.Count()).ToList();
            ViewBag.Countries = _db.Countries.Include("Products").OrderByDescending(i => i.Products.Count()).ToList();

            var updateProduct = _db.Products.Find(id);
            if (updateProduct != null)
            {
                return View(updateProduct);
            }
            return RedirectToAction(nameof(Edit));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Attach(model);
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Edit));
        }
        public ActionResult Delete(int? id)
        {
            var deleteProduct = _db.Products.Find(id);
            if (deleteProduct != null)
            {
                _db.Products.Remove(deleteProduct);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}