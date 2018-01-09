using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class User : ApplicationUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zipcode { get; set; }
        public bool IsEmployed { get; set; }
        public int MonthlyDebt { get; set; }
    }
}