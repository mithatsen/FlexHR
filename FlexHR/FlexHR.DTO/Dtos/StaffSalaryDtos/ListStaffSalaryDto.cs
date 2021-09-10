﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffSalaryDtos
{
    public class ListStaffSalaryDto    
    {
        public int StaffSalaryId { get; set; }
        public int StaffId { get; set; }
        public int CurrencyGeneralSubTypeId { get; set; }
        public int PeriodGeneralSubTypeId { get; set; } 
        public int FeeTypeGeneralSubTypeId { get; set; }
        public int TotalWorkingHour { get; set; }
        public decimal OvertimePayPerHour { get; set; }
        public decimal Salary { get; set; }
     
        public decimal? RoadPayment { get; set; }
        public decimal? FoodPayment { get; set; }
        public decimal? ConstantBonus { get; set; }
        public decimal? PrivateHealthCare { get; set; }
        public decimal? PrivatePension { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsAgi { get; set; }
        public bool IsActive { get; set; }

    }
}
