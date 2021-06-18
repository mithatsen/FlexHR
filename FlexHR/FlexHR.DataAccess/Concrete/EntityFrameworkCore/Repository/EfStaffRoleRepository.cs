using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfStaffRoleRepository : EfGenericRepository<StaffRole>, IStaffRoleDal
    {
        private readonly FlexHRContext _context;
        public EfStaffRoleRepository(FlexHRContext context) : base(context)
        {
            _context = context;
        }

        public StaffRole GetUserRoleByStaffId(int id)
        {
            return _context.StaffRole.Where(x => x.StaffId == id).FirstOrDefault();
        }
    }
}
