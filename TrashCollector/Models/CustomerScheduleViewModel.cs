using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class CustomerScheduleViewModel
    {
        public ApplicationUser User { get; set; }
        public List<Day> Days { get; set; }
    }
}