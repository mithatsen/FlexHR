using FlexHR.Entity.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class AppRole:IdentityRole<int>,ITable
    {
        public string Description { get; set; }
        public int AuthorizeTypeGeneralSubTypeId { get; set; }
        public bool IsActive { get; set; }
    }
}
