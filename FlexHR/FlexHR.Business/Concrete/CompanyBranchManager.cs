using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class CompanyBranchManager : GenericManager<CompanyBranch>, ICompanyBranchService
    {
        private readonly ICompanyBranchDal _companyBranchDal;
        public CompanyBranchManager(ICompanyBranchDal companyBranchDal) : base(companyBranchDal)
        {
            _companyBranchDal = companyBranchDal;
        }

        public List<CompanyBranch> GetCompanyBranchListByCompanyId(int id)
        {
            return _companyBranchDal.GetCompanyBranchListByCompanyId(id);
        }
    }
}
