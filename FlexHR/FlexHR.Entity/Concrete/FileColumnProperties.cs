using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class FileColumnProperties
    {
        public int Id { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDesc { get; set; }
        public bool IsActive { get; set; }
    }
}
