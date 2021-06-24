﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffLeaveDtos
{
    public class AddStaffLeaveDto
    {
        public int StaffId { get; set; }
        public int GeneralStatusGeneralSubTypeId { get; set; }
        public int LeaveTypeGeneralSubTypeId { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public string Description { get; set; }
        public bool IsSentForApproval { get; set; } = false;
        public bool IsMailSentToStaff { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
