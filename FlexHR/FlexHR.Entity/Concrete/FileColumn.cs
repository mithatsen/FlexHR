﻿using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class FileColumn : ITable
    {
        public int Id { get; set; }
        public int FileTypeId { get; set; }
        public string ColumnName { get; set; }
        public string ColumnDescription { get; set; }
        public int ColumnSequence { get; set; }
        public string DataType { get; set; }
        public bool AllowNulls { get; set; }
        public bool IsActive { get; set; }
        public FileType FileType { get; set; }
        public List<FileColumn_FileColumnProperties> FileColumn_FileColumnProperties { get; set; }

    }
}
