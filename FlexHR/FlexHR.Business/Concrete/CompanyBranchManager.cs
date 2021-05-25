using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class CompanyBranchManager : GenericManager<CompanyBranch>, ICompanyBranchService
    {
        public CompanyBranchManager(ICompanyBranchDal companyBranchDal) : base(companyBranchDal)
        {

        }
    }
}
