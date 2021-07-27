using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.LeaveDtos
{
    public class ListLeaveInfoAllStaffDto
    {
        public int StaffId { get; set; }
        public string NameSurname { get; set; }
        public string IdNumber { get; set; }
        public string CompanyName { get; set; }
        public string CompanyBranchName { get; set; }
        public string DepartmantName { get; set; }
        public string TitleName { get; set; }
        public DateTime JobStartDate { get; set; }
        public int TotalDayDeserved { get; set; }
        public int TotalDayUsed { get; set; }
        public int TotalDayRemain { get; set; }
        public bool IsActive { get; set; }

    }
}
