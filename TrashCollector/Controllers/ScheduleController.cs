using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class ScheduleController : Controller
    {
        //[Authorize]

        // GET: Schedule
        public ActionResult Index()
        {
            var user = new User()
            {
                FullName = "JoKo",
                Email = "Ji@me.com",
                Password = "123",
                Address = "132",
                City = "Mil",
                State = "WI",
                Zipcode = 4321,
                IsEmployed = false,
                MonthlyDebt = 0,
                Days = new List<Day>
                {
                    new Day {Date = DateTime.Now.Date, HasPickUp = false},

                }
            };
            var WorkSchedule = new WorkScheduleViewModel()
            {
                Employee = new User()
                {
                    FullName = "Jojo",
                    Email = "Je@me.com",
                    Password = "133",
                    Address = "134",
                    City = "Mile",
                    State = "WE",
                    Zipcode = 4321,
                    IsEmployed = false,
                    MonthlyDebt = 0,
                },
                Customers = new List<User>
                {
                   new User{
                             FullName = "JoKo",
                             Email = "Ji@me.com",
                             Password = "123",
                             Address = "132",
                             City = "Mil",
                             State = "WI",
                             Zipcode = 4321,
                             IsEmployed = false,
                             MonthlyDebt = 0,
                             Days = new List<Day>
                                {
                                     new Day {Date = DateTime.Now.Date, HasPickUp = true},

                                }
                           },
                }

            };
            return View(user);
        }
    }
}