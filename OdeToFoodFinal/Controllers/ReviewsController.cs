using OdeToFoodFinal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFoodFinal.Controllers
{
    public class ReviewsController : Controller
    {
        OdeToFoodFinalDb _db = new OdeToFoodFinalDb();

        //[ChildActionOnly] //not legal to call directly through http request
        //public ActionResult BestReview()
        //{
        //    var bestReview = from r in _reviews
        //                     orderby r.Rating descending
        //                     select r;

        //    return PartialView("_Review", bestReview.First());
        //}
        // GET: Reviews
        //Bind this actions expects id, but we alias it with restaurantId using the bind
        public ActionResult Index([Bind(Prefix = "id")] int restaurantId)
        {
            var restaurant = _db.Restaurants.Find(restaurantId);
            if(restaurant != null)
            {
                return View(restaurant);
            }
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult Create(int restaurantId)
        {
            return View();
        }
        //model binding
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RestaurantReview review)
        {
            if (ModelState.IsValid)
            {
                _db.Reviews.Add(review);
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = review.RestaurantId });
            }
            return View(review);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _db.Reviews.Find(id);
            return View(model);
        }
        [HttpPost]
        //to not edit the reviewername field
        //[Bind(Exclude = "ReviewerName")]
        public ActionResult Edit(RestaurantReview review)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(review).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = review.RestaurantId });
            }
            return View(review);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    _db.Dispose();
        //    base.Dispose(disposing);
        //}


        //static List<RestaurantReview> _reviews = new List<RestaurantReview>
        //{
        //    new RestaurantReview
        //    {
        //        Id = 1,
        //        Name = "Cinnamon Club",
        //        City = "London",
        //        Country = "UK",
        //        Rating = 10

        //    },
        //    new RestaurantReview
        //    {
        //        Id = 2,
        //        Name = "Marrakesh",
        //        City = "D.C",
        //        Country = "USA",
        //        Rating = 10

        //    },
        //    new RestaurantReview
        //    {
        //        Id = 3,
        //        Name = "The House of Elliot",
        //        City = "Ghent",
        //        Country = "Belgium",
        //        Rating = 10

        //    }
        //};



    }
}
