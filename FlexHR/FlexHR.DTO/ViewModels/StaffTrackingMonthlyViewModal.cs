using FlexHR.DTO.Dtos.ColorCodeDtos;
using FlexHR.DTO.Dtos.StaffTracking;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.ViewModels
{
    public class StaffTrackingMonthlyViewModal
    {
        public List<ListStaffTimeKeepingDto> ListStaffTimeKeepings { get; set; }
        public List<PublicHoliday> PublicHolidays { get; set; }
        public List<ColorCode> ColorCodes { get; set; }
        public DateTime filterDate { get; set; }
    }
}
