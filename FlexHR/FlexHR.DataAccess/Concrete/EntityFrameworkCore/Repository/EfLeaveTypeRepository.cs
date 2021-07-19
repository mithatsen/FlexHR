using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfLeaveTypeRepository : EfGenericRepository<LeaveType>, ILeaveTypeDal
    {
        private readonly FlexHRContext _context;
        public EfLeaveTypeRepository(FlexHRContext context) : base(context)
        {
            _context = context;
        }

    }
}
