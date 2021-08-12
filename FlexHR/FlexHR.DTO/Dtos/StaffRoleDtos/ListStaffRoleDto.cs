using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffRoleDtos
{
    public class ListStaffRoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AuthorizeTypeId { get; set; }
        public int IsActive { get; set; }
    }
}
