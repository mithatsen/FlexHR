using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfStaffOtherInfoRepository : EfGenericRepository<StaffOtherInfo>, IStaffOtherInfoDal
    {
        private readonly FlexHRContext _context;
        public EfStaffOtherInfoRepository(FlexHRContext context) : base(context)
        {
            _context = context;
        }

        public StaffOtherInfo GetOtherInfoByStaffId(int id)
        {
            return _context.StaffOtherInfo.Where(x => x.StaffId == id).FirstOrDefault();
        }

    }

}

