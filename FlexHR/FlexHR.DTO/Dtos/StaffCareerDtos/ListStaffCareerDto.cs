using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffCareerDtos
{
    public class ListStaffCareerDto
    {
        public int StaffCareerId { get; set; }
        public int StaffId { get; set; }
        public int CompanyId { get; set; }
        public int? CompanyBranchId { get; set; }
        public int DepartmantGeneralSubTypeId { get; set; }
        public int TitleGeneralSubTypeId { get; set; }
        public int ModeOfOperationGeneralSubTypeId { get; set; }
        public DateTime JobStartDate { get; set; }
        public DateTime? JobFinishDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsCareerContinue { get; set; }
        public string DepartmantName { get; set; }
        public string Title { get; set; }
        public string ModeOfOperation { get; set; }
        public string CompanyName { get; set; }
        public string  BranchName { get; set; }
   
    }
}

