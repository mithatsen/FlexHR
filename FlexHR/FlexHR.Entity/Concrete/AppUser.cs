using FlexHR.Entity.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class AppUser:IdentityUser<int>, ITable
    {
        public int StaffId { get; set; }
        public bool IsActive { get; set; }
        public Staff Staff { get; set; }
    }
}
