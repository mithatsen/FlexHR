using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.JobRotationDtos
{
    public class JobRotationAssignStaffDto
    {
        public int JobRotationId { get; set; }
        public List<int> StaffList { get; set; }
    }
}
