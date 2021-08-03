using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.RoleDtos
{
    public  class ListRoleDto
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
