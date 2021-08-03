using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.CompanyBranchDtos
{
    public class ListCompanyBranchDto
    {
        public int CompanyBranchId { get; set; }
        public int CompanyId { get; set; }
        public string BranchName { get; set; }
        public bool IsActive { get; set; }
        public string CompanyName { get; set; }
    }
}
