using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class UserDay
    {
        [Key]
        public int UserDayId { get; set; }
        public User User { get; set; }
        public Day Day { get; set; }

    }
}