﻿using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class PublicHoliday : ITable
    {
        public int PublicHolidayId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Description { get; set; }
        public decimal HolidayDuration { get; set; }
        public bool IsActive { get; set; }
    }
}