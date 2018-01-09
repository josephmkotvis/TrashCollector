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
            var _context = new ApplicationDbContext();
            var currentDayWorkSchedule = new WorkScheduleViewModel();
            var currentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            foreach(User user in _context.Users)
            {
                if (user.Id == currentUserId)
                {
                    currentDayWorkSchedule.Employee = user;
                }
            }
            foreach (UserDay userday in _context.UserDays)
            {
                foreach(Day day in _context.Days)
                {
                    if (userday.Day.Date == day.Date)
                    {
                        currentDayWorkSchedule.Customers.ToList();
                    }
                }
            }
                return View(currentDayWorkSchedule);
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