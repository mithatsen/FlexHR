using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.DashboardDtos
{
    public class ListStaffShiftOnDashboardDto
    {
        public int StaffShiftId { get; set; }
        public int StaffId { get; set; }
        public string NameSurname { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public int GenderGeneralSubTypeId { get; set; }
    }
}
