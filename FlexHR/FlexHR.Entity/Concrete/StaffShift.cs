using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class StaffShift : ITable
    {
        public int StaffShiftId { get; set; }
        public int StaffId { get; set; }
        public DateTime StartDate { get; set; }
        public string Description { get; set; }
        public int GeneralStatusGeneralSubTypeId { get; set; }
        public int ShiftHour { get; set; }
        public int ShiftMinute { get; set; }
        public bool IsSentForApproval { get; set; }
        public bool IsMailSentToStaff { get; set; }
        public int? WhoApprovedStaffId { get; set; }
        public bool IsActive { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
