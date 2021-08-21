using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class FileColumn_FileColumnProperties
    {
        public int Id { get; set; }
        public int FileColumnId { get; set; }
        public int FileColumnPropertiesId { get; set; }

        public FileColumn FileColumn { get; set; }
        public FileColumnProperties FileColumnProperties { get; set; }
    }
}
