using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.LeaveDtos
{
    public class ListLeaveInfoMonthlyDto
    {
        public int StaffId { get; set; }
        public string NameSurname { get; set; }
        public string IdNumber { get; set; }
        public string LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int TotalDay { get; set; }
        public string Description { get; set; }
        public int GeneralStatusId { get; set; }
    }
}
