﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffLeaveDtos
{
    public class ListStaffLeaveWithUserActiveInfoDto
    {
        public int StaffLeaveId { get; set; }
        public int StaffId { get; set; }
        public string NameSurname { get; set; }
        public int GeneralStatusGeneralSubTypeId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public string Description { get; set; }
        public bool IsSentForApproval { get; set; }
        public bool IsMailSentToStaff { get; set; }
        public bool IsActive { get; set; }
        public string StatusType { get; set; }
        public string LeaveType { get; set; }
        public int TotalDay { get; set; }
        public bool IsUserActive { get; set; }
    }
}
