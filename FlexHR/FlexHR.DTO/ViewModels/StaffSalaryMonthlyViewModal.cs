using FlexHR.DTO.Dtos.StaffSalaryDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.ViewModels
{
    public class StaffSalaryMonthlyViewModal
    {
        public List<ListStaffSalaryMonthlyDto> ListStaffSalaryMonthly { get; set; }
        public DateTime filterDate { get; set; }
    }
}
