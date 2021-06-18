using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class Role : ITable
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<StaffRole> StaffRole { get; set; }
    }
}
