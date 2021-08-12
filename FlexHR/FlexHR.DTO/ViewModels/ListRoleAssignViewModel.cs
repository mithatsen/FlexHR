using FlexHR.DTO.Dtos.RoleDtos;
using FlexHR.DTO.Dtos.StaffDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.ViewModels
{
    public class ListRoleAssignViewModel
    {
        public ListScreenDto ListScreenDto { get; set; }
        public ListRoleDto ListRoleDto { get; set; }
        public ListStaffDto ListStaffDto { get; set; }
    }
}
