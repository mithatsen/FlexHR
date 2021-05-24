using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class StaffCareer : ITable
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
        public bool IsActive { get; set; }

        public virtual CompanyBranch CompanyBranch { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
