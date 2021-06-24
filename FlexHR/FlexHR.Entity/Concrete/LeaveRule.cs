using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class LeaveRule
    {
        public int RuleId { get; set; }
        public int MinYear { get; set; }
        public int MaxYear { get; set; }
        public int AnnualLeaveCount { get; set; }
        public bool IsActive { get; set; }

    }
}
