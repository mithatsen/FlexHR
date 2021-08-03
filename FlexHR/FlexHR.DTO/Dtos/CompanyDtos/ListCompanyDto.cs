using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.CompanyDtos
{
    public class ListCompanyDto
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public bool IsActive { get; set; }

    }
}
