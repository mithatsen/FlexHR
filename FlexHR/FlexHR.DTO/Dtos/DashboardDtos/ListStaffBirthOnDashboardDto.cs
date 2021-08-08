using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.DashboardDtos
{
    public class ListStaffBirthOnDashboardDto
    {
        public int Id { get; set; }
        public int GenderGeneralSubTypeId { get; set; }
        public int StaffId { get; set; }
        public string NameSurname { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
