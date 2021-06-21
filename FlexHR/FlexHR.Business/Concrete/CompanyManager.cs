using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class CompanyManager : GenericManager<Company>, ICompanyService
    {
        private readonly ICompanyDal _companyDal;
        public CompanyManager(ICompanyDal companyDal) : base(companyDal)
        {
            _companyDal = companyDal;
        }

        public string GetCompanyNameByCompanyId(int id)
        {
            return _companyDal.GetCompanyNameByCompanyId(id);
        }
    }
}
