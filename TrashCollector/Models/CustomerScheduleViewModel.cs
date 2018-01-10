using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class CustomerScheduleViewModel
    {
        public ApplicationUser User { get; set; }
        public List<UserDay> FutureDays { get; set; }
        public List<UserDay> PastDays { get; set; }
        public List<UserDay> CanceledDays { get; set; }
    }
}