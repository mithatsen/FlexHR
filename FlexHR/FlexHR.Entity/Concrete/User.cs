using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class User : ITable
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string NameSurname { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
