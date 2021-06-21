using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfCompanyBranchRepository : EfGenericRepository<CompanyBranch>, ICompanyBranchDal
    {
        private readonly FlexHRContext _context;
        public EfCompanyBranchRepository(FlexHRContext context) : base(context)
        {
            _context = context;
        }

        public List<CompanyBranch> GetCompanyBranchListByCompanyId(int id)
        {
            return _context.CompanyBranch.Where(x => x.CompanyId == id).ToList();
        }
    }
}
