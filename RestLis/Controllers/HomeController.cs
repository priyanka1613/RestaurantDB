using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestLis.Models;
using System.Data;
using System.Threading;

namespace RestLis.Controllers
{
    public class HomeController : Controller
    {
        //string s;
        // GET: /RestaurantDB/
        private RestaurantDB res_db = new RestaurantDB();

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Index()
        {
            List<string> search_li = new List<string>();
            
                var search_li_location = res_db.res_infos.OrderBy(r => r.res_area)
                                       .Select(r => r.res_area);
                foreach (var i in search_li_location)
                {
                    string p = i.Split(' ').Last();
                    search_li.Add(p);
                }
                ViewBag.search_location = search_li.Distinct();
                ViewBag.search_catagory = res_db.res_infos.OrderBy(r => r.res_catagory)
                                        .Select(r => r.res_catagory).Distinct().ToList();

                ViewBag.page_number = 1;
                double x = res_db.res_infos.Count();
                ViewBag.res_count = Math.Ceiling(x / 5);
                var model = res_db.res_infos.ToList().OrderBy(r => r.res_name).Take(5);
                return View(model);
            
            
        }

        [HttpPost]
        public ActionResult Index(int x)
        {
            List<string> search_li = new List<string>();
            var search_li_location = res_db.res_infos.OrderBy(r => r.res_area)
                                   .Select(r => r.res_area);

            foreach (var i in search_li_location)
            {
                string p = i.Split(' ').Last();
                search_li.Add(p);
            }
            ViewBag.search_location = search_li.Distinct();
            ViewBag.search_catagory = res_db.res_infos.OrderBy(r => r.res_catagory)
                                    .Select(r => r.res_catagory).Distinct().ToList();

            double y = res_db.res_infos.Count();
            ViewBag.res_count = Math.Ceiling(y / 5);

            ViewBag.page_number = x;
            var model = res_db.res_infos.OrderBy(r => r.res_name).Skip((x - 1) * 5).Take(5);

            return View(model);
        }

        public ActionResult Index_search(string search_catagory, string search_location)
        {
            List<string> search_li = new List<string>();
            var search_li_location = res_db.res_infos.OrderBy(r => r.res_area)
                                   .Select(r => r.res_area);
            foreach (var i in search_li_location)
            {
                string p = i.Split(' ').Last();
                search_li.Add(p);
            }
            ViewBag.search_location = search_li.Distinct();
            ViewBag.search_catagory = res_db.res_infos.OrderBy(r => r.res_catagory)
                                    .Select(r => r.res_catagory).Distinct().ToList();

            if (search_location == "" && search_catagory == "")
            {
                return Redirect("~/");
            }
            if (search_location == "")
            {
                var model = res_db.res_infos.OrderBy(r => r.res_name)
                           .Where(r => r.res_catagory == search_catagory || search_catagory == null).ToList();
                int total = model.Count();
                ViewBag.total_res = total;
                return View(model);
            }
            if (search_catagory == "")
            {
                var model = res_db.res_infos.OrderBy(r => r.res_name)
                        .Where(r => r.res_area.Contains(search_location) || search_location == null).ToList();
                int total = model.Count();
                ViewBag.total_res = total;
                return View(model);
            }
            else
            {
                var model1 = res_db.res_infos.OrderBy(r => r.res_name)
                    .Where(r => r.res_catagory == search_catagory || search_catagory == null).ToList();
                var model = model1.OrderBy(r => r.res_name)
                            .Where(r => r.res_area.Contains(search_location)).ToList();
                int total = model.Count();
                ViewBag.total_res = total;
                return View(model);
            }
        }

        public ActionResult Details(int id)
        {
            var res_info = res_db.res_infos.Find(id);
            return View(res_info);
        }

        //
        // GET: /RestaurantDB/Create
        //[Authorize(Users = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /RestaurantDB/Create

        [HttpPost]
        public ActionResult Create(Restaurant_info res_info)
        {
            if (ModelState.IsValid)
            {
                res_info.res_picurl = "../../Content/Images/restaurants/" + res_info.res_name + ".jpg";
                res_db.res_infos.Add(res_info);
                res_db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /RestaurantDB/Edit/5
        //[Authorize(Users ="Admin")]
        public ActionResult Edit(int id)
        {
            var edit_item = res_db.res_infos.Find(id);
            return View(edit_item);
        }

        //
        // POST: /RestaurantDB/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Restaurant_info res_info_toedit)
        {
            if (ModelState.IsValid)
            {
                res_db.Entry(res_info_toedit).State = EntityState.Modified;
                res_db.SaveChanges();
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            return View(res_info_toedit);

        }

        //
        // GET: /RestaurantDB/Delete/5

        public ActionResult Delete(int id)
        {
            var model = res_db.res_infos.Find(id);
            return View(model);
        }

        //
        // POST: /RestaurantDB/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurant_info res_info_todelete = res_db.res_infos.Find(id);
            res_db.res_infos.Remove(res_info_todelete);
            res_db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult rating_Create(int id)
        {
            var n = res_db.res_infos.Find(id);
            ViewBag.r_name = n.res_name;
            try
            {
                double avg_rating = res_db.res_ratings
                                  .Where(p => p.res_name == n.res_name || n.res_name == null)
                                  .Select(p => p.ratings).Average();
                ViewBag.avg_rating = avg_rating;
            }
            catch
            {
                ViewBag.avg_rating = 0;
            }

            List<double> li = new List<double> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            ViewBag.li = li;

            var model = res_db.res_ratings
                    .OrderBy(r => r.res_name)
                    .Where(r => r.res_name == n.res_name || r.res_name == null);
            //ViewBag.rid=RouteData.Values["id"];
            ViewBag.Rating_Count = model.Count();
            Thread.Sleep(1000);
            return View();
        }

        [HttpPost]
        public ActionResult rating_Create(res_rating res_ratings, string id, double li)
        {
            res_ratings.ratings = li;
            res_ratings.res_name = id;
            res_ratings.date = DateTime.Today.Date.ToShortDateString();
            res_db.res_ratings.Add(res_ratings);
            res_db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Show_Ratings(string id)
        {

            var model = res_db.res_ratings
                    .OrderByDescending(r => r.date)
                    .Where(r => r.res_name == id);
            return PartialView(model);
        }
    }
}
