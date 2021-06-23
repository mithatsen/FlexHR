using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfStaffCareerRepository : EfGenericRepository<StaffCareer>, IStaffCareerDal
    {
        private readonly FlexHRContext _context;
        public EfStaffCareerRepository(FlexHRContext context) : base(context)
        {
            _context = context;
        }

        public List<StaffCareer> GetAllTableByStaffId(int id)
        {
            return _context.StaffCareer.Include(x=>x.CompanyBranch).ThenInclude(y=>y.Company)
                .Include(x=>x.Staff).ThenInclude(x => x.StaffGeneralSubType).ThenInclude(x => x.GeneralSubType)
                .ThenInclude(x => x.GeneralType).Where(p => p.StaffId == id).OrderByDescending(p=>p.JobStartDate).ToList();
        }
    }
}
