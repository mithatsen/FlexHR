﻿using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Interface
{
    public interface IStaffOtherInfoDal : IGenericDal<StaffOtherInfo>
    {
        StaffOtherInfo GetOtherInfoByStaffId(int id);
    }
}
