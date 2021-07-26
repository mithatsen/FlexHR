using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class LeaveRule:ITable
    {
        public int RuleId { get; set; }
        public int SeniorityYear { get; set; }     
        public int AditionalLeaveAmount { get; set; }
        public bool IsActive { get; set; }

    }
}

