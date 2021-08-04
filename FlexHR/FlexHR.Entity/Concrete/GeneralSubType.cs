using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class GeneralSubType: ITable
    {
        public int GeneralSubTypeId { get; set; }
        public int GeneralTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public virtual GeneralType GeneralType { get; set; }
      
    }
}
