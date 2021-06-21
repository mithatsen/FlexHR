using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfCompanyRepository : EfGenericRepository<Company>, ICompanyDal
    {
        private readonly FlexHRContext _context;
        public EfCompanyRepository(FlexHRContext context) : base(context)
        {
            _context = context;
        }

        public string GetCompanyNameByCompanyId(int id)
        {
            var result = _context.Company.Where(x => x.CompanyId == id).FirstOrDefault();
            return result.CompanyName;
        }
    }
}
