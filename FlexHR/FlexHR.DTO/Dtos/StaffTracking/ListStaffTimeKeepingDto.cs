using FlexHR.DTO.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffTracking
{
    public class ListStaffTimeKeepingDto
    {
        public int PersonalNo { get; set; }
        public string NameSurname { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public List<ColorCodeHelper> DaysStatus { get; set; }
        public List<ColorCodeHelper> ColorCodes { get; set; }
    
    }
}
