using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffShiftDtos
{
    public class ListStaffShiftWithUserActiveInfo
    {
        public int StaffShiftId { get; set; }
        public string NameSurname { get; set; }
        public int StaffId { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public int GeneralStatusGeneralSubTypeId { get; set; }
        public bool IsSentForApproval { get; set; }
        public bool IsMailSentToStaff { get; set; }
        public bool IsActive { get; set; }
        public bool IsUserActive { get; set; }
    }
}
