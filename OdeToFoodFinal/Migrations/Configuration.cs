namespace OdeToFoodFinal.Migrations
{
    using OdeToFoodFinal.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;

    internal sealed class Configuration : DbMigrationsConfiguration<OdeToFoodFinal.Models.OdeToFoodFinalDb>
    {
        public Configuration()
        {
            //to listen to new modification
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(OdeToFoodFinal.Models.OdeToFoodFinalDb context)
        {
            context.Restaurants.AddOrUpdate( r => r.Name,
            new Restaurant { Name = "Sabatino's", City = "Baltimore", Country = "USA" },
            new Restaurant { Name = "Greate Lake", City = "Chicago", Country = "USA" },
            new Restaurant
            {
                Name    = "Smaka",
                City    = "GothenBurg",
                Country = "Sweden",
                Reviews =
                    new List<RestaurantReview>{
                        new RestaurantReview { Rating = 9, Body="Great fodd!", ReviewerName="Scott" }
                    }
            });
             

            for (int count = 0; count < 1000; count++)
            {
                context.Restaurants.AddOrUpdate(r => r.Name,
                     new Restaurant { Name = count.ToString(), City = "Middle of Nowhere", Country = "USA" }
                );
            }

             
        }

       
    }
}
