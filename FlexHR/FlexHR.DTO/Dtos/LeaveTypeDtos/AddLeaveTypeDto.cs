using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.LeaveTypeDtos
{
    public class AddLeaveTypeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalDay { get; set; }
        public bool IsFree { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
