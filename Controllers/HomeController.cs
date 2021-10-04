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
    public class HomeController : Controller
    {
        ApplicationDbContext _db;
        public HomeController()
        {
            _db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _SearchProductPartial()
        {
            return PartialView();
        }
        public ActionResult SearchResult(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                return View(_db.Products.Include("Category").Include("Country").Where(i => i.Name.Contains(key)).OrderBy(i => i.Stock).ToList());
            }
            else
            {
                return RedirectToAction("Index", "Product");
            }
        }
        public ActionResult Shorting(string Sorting_Order)
        {
            ViewBag.SortingPrice = String.IsNullOrEmpty(Sorting_Order) ? "ByPrice" : "";
            ViewBag.SortingStock = String.IsNullOrEmpty(Sorting_Order) ? "ByStock" : "";
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "ByName" : "";

            ViewBag.SortingPriceDescending = String.IsNullOrEmpty(Sorting_Order) ? "ByPriceDescending" : "";
            ViewBag.SortingStockDescending = String.IsNullOrEmpty(Sorting_Order) ? "ByStockDescending" : "";
            ViewBag.SortingNameDescending = String.IsNullOrEmpty(Sorting_Order) ? "ByNameDescending" : "";

            var products = from x in _db.Products select x;
            switch (Sorting_Order)
            {
                case "ByPrice":
                    products = products.OrderBy(i => i.Price);
                    break;
                case "ByPriceDescending":
                    products = products.OrderByDescending(i => i.Price);
                    break;
                case "ByName":
                    products = products.OrderBy(i => i.Name);
                    break;
                case "ByNameDescending":
                    products = products.OrderByDescending(i => i.Name);
                    break;
                case "ByStock":
                    products = products.OrderBy(i => i.Stock);
                    break;
                case "ByStockDescending":
                    products = products.OrderByDescending(i => i.Stock);
                    break;
                default:
                    products = products.OrderByDescending(i => i.CreatedDate);
                    break;
            }
            return View(products.ToList());
        }
        public ActionResult ProductFiltering()
        {
            return View(_db.Products.Include("Category").Include("Country").OrderBy(i => i.Stock).ToList());
        }
        public ActionResult AdvancedSearchResult(string key, string country, string category)
        {
            var searchResult = _db.Products.Include("Category").Include("Country")
                 .Where(i => i.Category.Name.Contains(category) && i.Country.Name.Contains(country) && i.Status.Contains(key))
                 .OrderByDescending(i => i.CreatedDate).ToList();
            return View(searchResult);
        }
        public ActionResult _CategoryList()
        {
            return View(_db.Categories.Include("Products").Where(i => i.Products.Count() > 0).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult _CountryList()
        {
            return View(_db.Countries.Include("Products").Where(i => i.Products.Count() > 0).OrderByDescending(i => i.CreatedDate).ToList());
        }
    }
}