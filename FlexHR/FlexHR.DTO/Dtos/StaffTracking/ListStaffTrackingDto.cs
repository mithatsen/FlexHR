using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffTracking
{
    public class ListStaffTrackingDto
    {
        public DateTime UploadDate { get; set; }
        public int CardNo { get; set; }
        public string EnterTime { get; set; }
        public string ExitTime { get; set; }
        public string Status { get; set; }
    }
}
