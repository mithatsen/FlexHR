using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.RoleDtos
{
    public class AddMultipleRoleToStaffDto
    {
        public int StaffId { get; set; }
        public List<int> Roles { get; set; }
        public List<int> Permissions { get; set; }
    
      
    }
}
