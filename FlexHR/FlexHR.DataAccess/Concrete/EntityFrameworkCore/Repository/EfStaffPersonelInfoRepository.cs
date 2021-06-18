using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfStaffPersonelInfoRepository : EfGenericRepository<StaffPersonelInfo>, IStaffPersonelInfoDal
    {
        private readonly FlexHRContext _context;
        public EfStaffPersonelInfoRepository(FlexHRContext context) : base(context)
        {
            _context = context;
        }

        public StaffPersonelInfo GetPersonelInfoByStaffId(int id)
        {
            return _context.StaffPersonelInfo.Where(x => x.StaffId == id).FirstOrDefault();
        }
    }
}
