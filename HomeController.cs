using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCGridView.Models;

namespace MVCGridView.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        OKEntities db = new OKEntities();
        public ActionResult Index(int? pageNumber)
        {
            ProductModel model = new ProductModel();
            model.PageNumber = (pageNumber == null ? 1 : Convert.ToInt32(pageNumber));
            model.PageSize = 4;

            List<Product> products = db.Products.ToList();

            if (products != null)   
            {
                model.Products = products.OrderBy(x => x.id)
                         .Skip(model.PageSize * (model.PageNumber - 1))
                         .Take(model.PageSize).ToList();

                model.TotalCount = products.Count();
                var page = (model.TotalCount / model.PageSize) - (model.TotalCount % model.PageSize == 0 ? 1 : 0);
                model.PagerCount = page + 1;
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult WebGrid()
        {
            ProductModel model = new ProductModel();
            model.PageSize = 4;

            List<Product> products = db.Products.ToList();

            if (products != null)
            {
                model.TotalCount = products.Count();
                model.Products = products;
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult jqGrid()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AngularJS()
        {
            return View();
        }

        public ActionResult GetProducts(string sidx, string sord, int page, int rows)
        {
            var products = db.Products.ToList();
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            int totalRecords = products.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            
            var data = products.OrderBy(x => x.id)
                         .Skip(pageSize * (page - 1))
                         .Take(pageSize).ToList();

            var jsonData = new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = data
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductById(int id)
        {
            var products = db.Products.Where(x => x.id == id);

            if (products != null)
            {
                Product model = new Product();

                foreach (var item in products)
                {
                    model.id = item.id;
                    model.name = item.name;
                    model.price = Convert.ToDecimal(item.price);
                    model.department = item.department;
                }

                return PartialView("_GridEditPartial", model);
            }

            return View();
        }

   
        public ActionResult DeleteById(int id)
        {
            var model1 = db.Products.Where(x => x.id == id);

            if (model1 != null)
            {
                Product model = new Product();

                foreach (var item in model1)
                {
                    model.id = item.id;
                    model.name = item.name;
                    model.price = Convert.ToDecimal(item.price);
                    model.department = item.department;
                }

                return PartialView("_GridDeletePartial", model);
            }

            return View();
        }
        [HttpDelete]
        public ActionResult Delete1(Product model)
        {
            Product model1 = db.Products.Find(model.id);

            
                db.Products.Remove(model1);
                db.SaveChanges();
                return RedirectToAction("Index");
          
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product model)
        {
            //update database
            Product original = db.Products.Find(model.id);
            if (original != null)
            {
                db.Entry(original).CurrentValues.SetValues(model);
                db.SaveChanges();
            }
          
            
            return Content("Record updated!!", "text/html");
        }
    }
}
