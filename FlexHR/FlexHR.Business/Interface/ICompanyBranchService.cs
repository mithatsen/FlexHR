using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Interface
{
    public interface ICompanyBranchService : IGenericService<CompanyBranch>
    {
        public List<CompanyBranch> GetCompanyBranchListByCompanyId(int id);
    }
}
