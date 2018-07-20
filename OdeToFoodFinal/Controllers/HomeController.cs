using OdeToFoodFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace OdeToFoodFinal.Controllers
{
 
    public class HomeController : Controller
    {
        OdeToFoodFinalDb _db = new OdeToFoodFinalDb();

        public ActionResult Autocomplete(string term)
        {
            var model =
                _db.Restaurants
                   .Where(r => r.Name.StartsWith(term))
                   .Take(10)
                   .Select(r => new
                   {
                       label = r.Name
                   });
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [OutputCache(Duration = 60)]
        public ActionResult Index(string searchTerm = null, int page = 1)
        {
            //var model = _db.Restaurants.ToList();

            //Comprehension Query Syntax
            //var model =
            //    from r in _db.Restaurants
            //        //orderby r.Reviews.Count() descending
            //    orderby r.Reviews.Average(review => review.Rating) descending
            //    select new RestaurantListViewModel //carry the information along that the view need
            //    {
            //        Id = r.Id,
            //        Name = r.Name,
            //        City = r.City,
            //        Country = r.Country,
            //        CountOfReviews = r.Reviews.Count()
            //    };

            //Extension snytax
            var model = 
                _db.Restaurants
                    .OrderByDescending (r => r.Reviews.Average(review => review.Rating))
                    .Where(r => searchTerm == null || r.Name.StartsWith(searchTerm))
                    //.Take(10)
                    .Select( r=> new RestaurantListViewModel //carry the information along that the view need
                    {
                        Id = r.Id,
                        Name = r.Name,
                        City = r.City,
                        Country = r.Country,
                        CountOfReviews = r.Reviews.Count()
                    }).ToPagedList(page, 10);

            if(Request.IsAjaxRequest())
            {
                return PartialView("_Restaurants", model);
            }

            return View(model);
        }
        //need to login before to use this action

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //close resource that might be open string
        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}