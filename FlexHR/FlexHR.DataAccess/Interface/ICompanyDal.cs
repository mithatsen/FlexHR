﻿using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Interface
{
    public interface ICompanyDal : IGenericDal<Company>
    {
        string GetCompanyNameByCompanyId(int id);
    }
}
