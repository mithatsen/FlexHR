using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffLeaveDtos
{
    public class AddStaffLeaveMultipleDto
    {
        public List<string> StaffId { get; set; }
        public int GeneralStatusGeneralSubTypeId { get; set; } = 96;
        public int LeaveTypeId { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public string Description { get; set; }
        public int TotalDay { get; set; }
        public bool IsSentForApproval { get; set; } = false;
        public bool IsMailSentToStaff { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public bool IsCheckedApprove { get; set; }
        public int? WhoApprovedStaffId { get; set; }
    }
}
