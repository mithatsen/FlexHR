using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class CompanyFile : ITable
    {
        public int CompanyFileId { get; set; }
        public int FileGeneralSubTypeId { get; set; }
        public string FileName { get; set; }
        public string FileFullPath { get; set; }
        public bool IsActive { get; set; }
    }
}
