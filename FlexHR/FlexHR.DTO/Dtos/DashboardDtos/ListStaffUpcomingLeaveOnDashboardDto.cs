using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.DashboardDtos
{
    public class ListStaffUpcomingLeaveOnDashboardDto
    {
        public int StaffLeaveId { get; set; }
        public int StaffId { get; set; }
        public int GenderGeneralSubTypeId { get; set; }
        public string NameSurname { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public string Description { get; set; }
        public string LeaveType { get; set; }
        public int TotalDay { get; set; }
    }
}
