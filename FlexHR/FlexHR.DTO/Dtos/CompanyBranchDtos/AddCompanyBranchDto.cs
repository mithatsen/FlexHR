using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.CompanyBranchDtos
{
    public class AddCompanyBranchDto
    {
      
        public int CompanyId { get; set; }
        public string BranchName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
