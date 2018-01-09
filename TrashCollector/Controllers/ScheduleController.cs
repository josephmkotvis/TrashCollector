using System;
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
        public ActionResult Index()
        {
            var user = System.Web.HttpContext.Current.User;
            return View(user );
        }
        public ActionResult EmployeeWorkSchedule()
        {
            var db = new ApplicationDbContext();
            var todaySchedule = new WorkScheduleViewModel();
            foreach (UserDay userday in db.UserDays)
            {
                foreach(Day day in db.Days)
                {
                    if (userday.Day.Date == day.Date)
                    {
                        todaySchedule.Customers.ToList();
                    }
                }
            }
            todaySchedule.Employee = db.Users.FirstOrDefault();
                return View();
        }
        public ActionResult AddPickUp()
        {
            return View();
        }
        public ActionResult RemovePickUp()
        {
            return View();
        }
    }
}