using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RestLis.Models;

namespace RestLis.Models
{
    public class RestaurantDB : DbContext
    {
        public DbSet<Restaurant_info> res_infos { get; set; }
        public DbSet<res_rating> res_ratings { get; set; }
    }
}