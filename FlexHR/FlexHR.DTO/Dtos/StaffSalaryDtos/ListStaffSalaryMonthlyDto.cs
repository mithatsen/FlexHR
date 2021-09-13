using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffSalaryDtos
{
    public class ListStaffSalaryMonthlyDto
    {
        public string NameSurname { get; set; }
        public int CardNo { get; set; }
        public string TotalWorkingHour { get; set; }
        public string  TotalOvertimeHour { get; set; }
        public string Branch { get; set; }
        public string CurrencyTypeName { get; set; }
        public string Department { get; set; }
        public decimal? IncomePerHour { get; set; }
        public decimal? IncomePerShiftHour { get; set; }
        public decimal? TotalDeservedSalary { get; set; }
        public decimal? NetSalary { get; set; }

    }
}
