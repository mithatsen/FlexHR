using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.LeaveRuleDtos
{
    public class ListLeaveRuleDto
    {
        public int RuleId { get; set; }
        public int SeniorityYear { get; set; }
        public int AditionalLeaveAmount { get; set; }
        public bool IsActive { get; set; }
    }
}
