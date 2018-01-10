using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class MapController : Controller
    {
        ApplicationDbContext db;
        public MapController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Map
        public ActionResult Index(int id)
        {
            var day = (from x in db.UserDays.Include("User") where x.UserDayId == id select x).FirstOrDefault();
            var splitAddress = day.User.Address.Split(' ');
            var formattedAddress = string.Join("+", splitAddress);
            string address = formattedAddress + ",+" + day.User.Zipcode;
            MapViewModel model = new MapViewModel();
            model.Address = address;
            return View(model);
        }
    }
}