using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.CompanyDtos
{
    public class AddCompanyDto
    {
        public string CompanyName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
