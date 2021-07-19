using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class LeaveTypeManager : GenericManager<LeaveType>, ILeaveTypeService
    {
        private readonly ILeaveTypeDal _leaveTypeDal;

        public LeaveTypeManager(ILeaveTypeDal leaveTypeDal) : base(leaveTypeDal)
        {
            _leaveTypeDal = leaveTypeDal;

        }
    }
}
