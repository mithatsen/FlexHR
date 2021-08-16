using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Interface
{
    public interface IAppUserDal : IGenericDal<AppUser>
    {
        List<AppRole> GetAppRolesByStaffId(int id);
        List<AppRole> GetAppRolesByUserId(int id);
    }
}
