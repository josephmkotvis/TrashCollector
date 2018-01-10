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
        ApplicationDbContext db;
        public ScheduleController()
        {
            db = new ApplicationDbContext();
        }
        [Authorize]
        public ActionResult Index()
        {
            var currentSchedule = new CustomerScheduleViewModel();
            var currentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            foreach(ApplicationUser user in db.Users)
            {
                if (user.Id == currentUserId)
                {
                    currentSchedule.User = user;
                }
            }
            currentSchedule.PastDays = new List<UserDay>();
            currentSchedule.FutureDays = new List<UserDay>();
            currentSchedule.CanceledDays = new List<UserDay>();
            var Days = (from x in db.UserDays.Include("Day") where x.User.Id == currentUserId select x).ToList();
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

            var currentDayWorkSchedule = new WorkScheduleViewModel();
            var currentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            foreach(ApplicationUser user in db.Users)
            {
                if (user.Id == currentUserId)
                {
                    currentDayWorkSchedule.Employee = user;
                }
            }
            currentDayWorkSchedule.CustomersAndDays = new List<UserDay>();
            var Days = (from x in db.UserDays.Include("Day") where x.User.Zipcode == currentDayWorkSchedule.Employee.Zipcode select x).ToList();
            foreach (UserDay userday in Days)
            {
                if (userday.HasPickUpRequested == true && userday.WasPickedUp == false && userday.Day.Date.Date == DateTime.Now.Date) 
                {
                    currentDayWorkSchedule.CustomersAndDays.Add(userday);
                }
            }
                return View(currentDayWorkSchedule);
        }
        [HttpGet]
        public ActionResult AddPickUp()
        {
            AddPickUpViewModel model = new AddPickUpViewModel();
            model.UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddPickUp (AddPickUpViewModel model)
        {
            var newPickUp = new UserDay
            {
                User = (from x in db.Users where x.Id == model.UserId select x).FirstOrDefault(),
                Day = GetDate(model.SelectedDate)
                };
            newPickUp.HasPickUpRequested = true;
            newPickUp.WasPickedUp = false;
            db.UserDays.Add(newPickUp);
            db.SaveChanges();
            return RedirectToAction("Index", "Schedule");
        }

        private Day GetDate(string selectedDate)
        {
            DateTime date = DateTime.Parse(selectedDate);
            var dayList = (from x in db.Days where x.Date == date select x).ToList();
            if(dayList.Count > 0)
            {
                return dayList[0];
            }
            else
            {
                Day day = new Day();
                day.Date = date;
                db.Days.Add(day);
                db.SaveChanges();
                var returnDay = (from x in db.Days where x.Date == date select x).FirstOrDefault();
                return returnDay;
            }
        }

        public ActionResult CustomerRemovePickUp(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var RemovedPickUp = (from x in db.UserDays where x.UserDayId == id select x).FirstOrDefault();
            RemovedPickUp.HasPickUpRequested = false;
            RemovedPickUp.WasPickedUp = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult EmployeeCompletePickUp(int id)
        {
            var RemovedPickUp = (from x in db.UserDays.Include("User") where x.UserDayId == id select x).FirstOrDefault();
            RemovedPickUp.HasPickUpRequested = true;
            RemovedPickUp.WasPickedUp = true;
            db.SaveChanges();
            BillCustomer(RemovedPickUp.User.Id);
            return RedirectToAction("EmployeeWorkSchedule", "Schedule");
        }

        private void BillCustomer(string id)
        {
            var user = (from x in db.Users where x.Id == id select x).FirstOrDefault();
            user.MonthlyDebt += 50;
            db.SaveChanges();
        }
        public ActionResult PayBalance()
        {
            var currentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var debtRemoved = (from x in db.Users where x.Id == currentUserId select x).FirstOrDefault();
            debtRemoved.MonthlyDebt = 0;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}