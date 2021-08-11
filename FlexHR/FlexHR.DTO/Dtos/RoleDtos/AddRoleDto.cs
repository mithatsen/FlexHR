using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.RoleDtos
{
    public class AddRoleDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AuthorizeTypeGeneralSubTypeId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
