using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class LeaveType
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Description { get; set; }
        public int TotalDay { get; set; }
        public bool IsFree { get; set; }
        public bool IsActive { get; set; }
       public virtual List<StaffLeave> StaffLeave { get; set; }

    }
}
