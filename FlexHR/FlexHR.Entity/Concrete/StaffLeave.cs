﻿using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class StaffLeave : ITable
    {
        public int StaffLeaveId { get; set; }
        public int StaffId { get; set; }
        public int LeaveTypeId { get; set; }
        public int GeneralStatusGeneralSubTypeId { get; set; }        
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public string Description { get; set; }
        public int TotalDay { get; set; }
        public int? WhoApprovedStaffId { get; set; }
        public bool IsSentForApproval { get; set; }
        public bool IsMailSentToStaff { get; set; }
        public bool IsActive { get; set; }

        public virtual Staff Staff { get; set; }
        public virtual LeaveType LeaveType { get; set; }

    }
}
