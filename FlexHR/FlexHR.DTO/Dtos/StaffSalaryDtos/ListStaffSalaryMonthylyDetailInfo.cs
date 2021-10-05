using FlexHR.DTO.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffSalaryDtos
{
    public class ListStaffSalaryMonthylyDetailInfo
    {
        public string IdNumber { get; set; }
        public string NameSurname { get; set; }
        public DateTime JobJoinDate { get; set; }
        public DateTime PayrollDate { get; set; }
        public string Branch { get; set; }
        public string Title { get; set; }
        public decimal PaymentPerHour { get; set; }
        public decimal SalaryWithoutAgi { get; set; }
        public decimal Agi { get; set; }
        public decimal SalaryWithAgi { get; set; }

        ////////////////////////////////////////////
        public StaffSalaryMonthlyHelper NormalWorking { get; set; }
        public StaffSalaryMonthlyHelper WeekBreak { get; set; }
        public StaffSalaryMonthlyHelper HolidaysWithPay { get; set; }
        public StaffSalaryMonthlyHelper Shift { get; set; }
        public StaffSalaryMonthlyHelper SundayShift { get; set; }
        public decimal Bonus { get; set; } //prim
        public decimal Perks { get; set; } //ikramiye
        public decimal AdditionIncome1 { get; set; } 
        public decimal AdditionIncome2 { get; set; }

        /////////////////////////////////////////////
        public StaffSalaryMonthlyHelper Absence { get; set; }
        public StaffSalaryMonthlyHelper HolidayWithoutPay { get; set; }
        public StaffSalaryMonthlyHelper Report { get; set; }
        public decimal AdvanceDeduction { get; set; }
        public decimal ExecutiveDeduction { get; set; } // icra kesintisi
        public decimal PrivatePensionDeduction { get; set; } 
        public decimal Deduction1 { get; set; } 
        public decimal Deduction2 { get; set; } 
    }
}
