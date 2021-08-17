﻿using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfCompanyFileRepository : EfGenericRepository<CompanyFile>, ICompanyFileDal
    {
        public EfCompanyFileRepository(FlexHRContext context) : base(context)
        {

        }
    }
}
