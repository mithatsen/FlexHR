using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class FileType
    {
        public int Id { get; set; }
        public string FileUploadTypeName { get; set; }
        public bool IsActive { get; set; }
        public List<FileColumn> FileColumn { get; set; }
    }
}
