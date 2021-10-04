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
    public class CountryController : Controller
    {
        ApplicationDbContext _db;
        public CountryController()
        {
            _db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View(_db.Countries.Include("Products").OrderByDescending(i => i.Products.Count()).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Country model)
        {
            if (ModelState.IsValid)
            {
                _db.Countries.Add(model);
                _db.Entry(model).State = EntityState.Added;
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Create));
        }
        public ActionResult Edit(int? id)
        {
            var updateCountry = _db.Countries.Find(id);
            if (updateCountry != null)
            {
                return View(updateCountry);
            }
            return RedirectToAction(nameof(Edit));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Country model)
        {
            if (ModelState.IsValid)
            {
                _db.Countries.Attach(model);
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Edit));
        }
        public ActionResult Delete(int? id)
        {
            var deleteCountry = _db.Countries.Find(id);
            if (deleteCountry != null)
            {
                _db.Countries.Remove(deleteCountry);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}