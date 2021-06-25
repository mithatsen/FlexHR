using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffFileDtos
{
    public class ListStaffFileDto
    {
        public int StaffFileId { get; set; }
        public int StaffId { get; set; }
        public int FileGeneralSubTypeId { get; set; }
        public string FileName { get; set; }
        public string FileFullPath { get; set; }
        public bool IsActive { get; set; }

    }
}
