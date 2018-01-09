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
            foreach (UserDay userday in _context.UserDays)
            {
                if ((userday.User.Id == currentUserId) && (userday.Day.Date == DateTime.Now.Date))
                {
                    currentSchedule.Days.Add(userday.Day);
                }
            }
            return View(currentSchedule);
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
                        currentDayWorkSchedule.CustomersAndDays.Add(userday);
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