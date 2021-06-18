using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class StaffRole : ITable
    {
        public int StaffRoleId { get; set; }
        public int StaffId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
