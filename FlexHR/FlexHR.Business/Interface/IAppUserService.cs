﻿using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Interface
{
    public  interface IAppUserService : IGenericService<AppUser>
    {
        List<AppRole> GetAppRolesByStaffId(int id);
        List<AppRole> GetAppRolesByUserId(int id);
    }
}
