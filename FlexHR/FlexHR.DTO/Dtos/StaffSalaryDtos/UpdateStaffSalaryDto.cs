using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffSalaryDtos
{
    public class UpdateStaffSalaryDto
    {
        public int StaffSalaryId { get; set; }
        public int StaffId { get; set; }
        public int CurrencyGeneralSubTypeId { get; set; }
        public int PeriodGeneralSubTypeId { get; set; }
        public int FeeTypeGeneralSubTypeId { get; set; }
        public decimal PayPerHour { get; set; }
        public decimal Salary { get; set; }
        public decimal? RoadPayment { get; set; } = 0;
        public decimal? FoodPayment { get; set; } = 0;
        public decimal? ConstantBonus { get; set; } = 0;
        public decimal? PrivateHealthCare { get; set; } = 0;
        public decimal? PrivatePension { get; set; } = 0;
        public DateTime StartDate { get; set; }
        public decimal? AgiPayment { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
