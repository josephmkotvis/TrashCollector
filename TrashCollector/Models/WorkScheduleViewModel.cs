﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TrashCollector.Models
{
    public class WorkScheduleViewModel
    {

        public User Employee { get; set; }
        public List<UserDay> CustomersAndDays { get; set; }
    }
}