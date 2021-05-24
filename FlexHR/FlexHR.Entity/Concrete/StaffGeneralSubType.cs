using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class StaffGeneralSubType : ITable
    {
        public int GeneralSubTypeStaffId { get; set; }
        public int GeneralSubTypeId { get; set; }
        public int StaffId { get; set; }

        public virtual GeneralSubType GeneralSubType { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
