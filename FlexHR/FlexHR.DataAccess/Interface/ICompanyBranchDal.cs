﻿using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Interface
{
    public interface ICompanyBranchDal : IGenericDal<CompanyBranch>
    {
        public List<CompanyBranch> GetCompanyBranchListByCompanyId(int id);
    }
}
