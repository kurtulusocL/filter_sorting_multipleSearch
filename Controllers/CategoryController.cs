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
    public class CategoryController : Controller
    {
        ApplicationDbContext _db;
        public CategoryController()
        {
            _db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View(_db.Categories.Include("Products").OrderByDescending(i => i.Products.Count()).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(model);
                _db.Entry(model).State = EntityState.Added;
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Create));
        }
        public ActionResult Edit(int? id)
        {
            var updateCategory = _db.Categories.Find(id);
            if (updateCategory != null)
            {
                return View(updateCategory);
            }
            return RedirectToAction(nameof(Edit));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Attach(model);
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Edit));
        }
        public ActionResult Delete(int? id)
        {
            var deleteCategory = _db.Categories.Find(id);
            if (deleteCategory != null)
            {
                _db.Categories.Remove(deleteCategory);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}