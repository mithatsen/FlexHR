using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffShiftDtos
{
    public class AddStaffShiftMultipleDto
    {
        public List<int> StaffId { get; set; }
        public DateTime StartDate { get; set; }
        public int ShiftHour { get; set; }
        public int ShiftMinute { get; set; }
        public string Description { get; set; }
        public int GeneralStatusGeneralSubTypeId { get; set; } = 96;
        public bool IsSentForApproval { get; set; }
        public bool IsMailSentToStaff { get; set; }
        public bool IsActive { get; set; }
        public bool IsCheckedApprove { get; set; }
        public int? WhoApprovedStaffId { get; set; }

    }
}
