using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class ScheduleController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var _context = new ApplicationDbContext();
            var currentSchedule = new CustomerScheduleViewModel();
            var currentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            foreach(ApplicationUser user in _context.Users)
            {
                if (user.Id == currentUserId)
                {
                    currentSchedule.User = user;
                }
            }
            currentSchedule.PastDays = new List<UserDay>();
            currentSchedule.FutureDays = new List<UserDay>();
            currentSchedule.CanceledDays = new List<UserDay>();
            var Days = (from x in _context.UserDays where x.User.Id == currentUserId select x).ToList();
            foreach (UserDay userday in Days)
            {
                if (userday.HasPickUpRequested == true && userday.WasPickedUp == false) 
                {
                    currentSchedule.FutureDays.Add(userday);
                }
                else if (userday.HasPickUpRequested == false && userday.WasPickedUp == false)
                {
                    currentSchedule.CanceledDays.Add(userday);
                }
                else if (userday.HasPickUpRequested == true && userday.WasPickedUp == true)
                {
                    currentSchedule.PastDays.Add(userday);
                }
            }
            return View(currentSchedule);
        }
        public ActionResult EmployeeWorkSchedule()
        {
            var _context = new ApplicationDbContext();
            var currentDayWorkSchedule = new WorkScheduleViewModel();
            var currentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            foreach(ApplicationUser user in _context.Users)
            {
                if (user.Id == currentUserId)
                {
                    currentDayWorkSchedule.Employee = user;
                }
            }
            currentDayWorkSchedule.CustomersAndDays = new List<UserDay>();
            foreach (UserDay userday in _context.UserDays)
            {
                foreach(Day day in _context.Days)
                {
                    if (userday.Day.Date == day.Date)
                    {
                        currentDayWorkSchedule.CustomersAndDays.Add(userday);
                    }
                }
            }
                return View(currentDayWorkSchedule);
        }
        [HttpGet]
        public ActionResult AddPickUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPickUp (AddPickUpViewModel RETURNFORM)
        {
            ApplicationDbContext db = new ApplicationDbContext();
                var newPickUp = new UserDay
                {
                    User = (from x in db.Users where x.Id == RETURNFORM.UserId select x).FirstOrDefault(),
                    Day = RETURNFORM.Day,
                };
            newPickUp.HasPickUpRequested = true;
            newPickUp.WasPickedUp = false;
            db.UserDays.Add(newPickUp);
            db.SaveChanges();
            return RedirectToAction("Index", "Schedule");
        }
        public ActionResult CustomerRemovePickUp(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var RemovedPickUp = (from x in db.UserDays where x.UserDayId == id select x).FirstOrDefault();
            RemovedPickUp.HasPickUpRequested = true;
            RemovedPickUp.WasPickedUp = false;
            db.SaveChanges();
            return View();
        }
        public ActionResult EmployeeCompletePickUp(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var RemovedPickUp = (from x in db.UserDays where x.UserDayId == id select x).FirstOrDefault();
            RemovedPickUp.HasPickUpRequested = true;
            RemovedPickUp.WasPickedUp = true;
            db.SaveChanges();
            return View();
        }
    }
}