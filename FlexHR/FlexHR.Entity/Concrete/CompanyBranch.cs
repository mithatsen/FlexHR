using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class CompanyBranch: ITable
    {
        public int CompanyBranchId { get; set; }
        public int CompanyId { get; set; }
        public string BranchName { get; set; }
        public bool IsActive { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<StaffCareer> StaffCareer { get; set; }
    }
}
