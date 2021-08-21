using FlexHR.Entity.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.ViewModels
{
    public class FileUploadViewModel
    {
        public IFormFile file { get; set; }
        public int fileUploadTypeID { get; set; }
        public string folderName { get; set; }
        public string xlsPath { get; set; }
        public string xlsFileName { get; set; }
        public string tableName { get; set; }
        public DateTime UploadDate { get; set; }
        public List<string[]> rows { get; set; }
        public List<FileColumn> columnList { get; set; }
        public int columnCount { get { return columnList.Count; } set { } }
    }
}
