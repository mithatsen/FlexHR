using FlexHR.DTO.Dtos.StaffTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.ViewModels
{
    public class StaffTrackingMonthlyViewModal
    {
        public List<ListStaffTimeKeepingDto> ListStaffTimeKeepings { get; set; }
        public DateTime filterDate { get; set; }
    }
}
