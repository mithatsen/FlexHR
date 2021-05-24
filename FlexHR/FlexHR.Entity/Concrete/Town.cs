using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class Town : ITable
    {
        public int TownId { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<StaffOtherInfo> StaffOtherInfo { get; set; }
    }
}
