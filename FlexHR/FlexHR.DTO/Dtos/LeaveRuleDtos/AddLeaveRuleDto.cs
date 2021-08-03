using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.LeaveRuleDtos
{
    public class AddLeaveRuleDto
    {
        public int SeniorityYear { get; set; }
        public int AditionalLeaveAmount { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
