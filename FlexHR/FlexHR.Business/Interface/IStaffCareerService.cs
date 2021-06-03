﻿using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Interface
{
    public interface IStaffCareerService : IGenericService<StaffCareer>
    {
        List<StaffCareer> GetAllTableByStaffId(int id);
    }
}
