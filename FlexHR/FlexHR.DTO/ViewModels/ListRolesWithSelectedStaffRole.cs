using FlexHR.Entity.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;


namespace FlexHR.DTO.ViewModels
{
    public class ListRolesWithSelectedStaffRole
    {
        public bool IsUser { get; set; }
        public List<AppRole> Roles { get; set; }
        public List<AppRole> Permissions { get; set; }
        public List<AppRole> UserRoles { get; set; }
        public List<AppRole> UserPermissions { get; set; }

    }
}
