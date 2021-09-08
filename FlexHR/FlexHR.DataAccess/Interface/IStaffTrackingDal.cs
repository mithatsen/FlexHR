using FlexHR.DTO.Dtos.StaffTracking;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Interface
{
    public interface IStaffTrackingDal
    {
        List<ListStaffTimeKeepingDto> GetStaffTimeKeepingMonthly(DateTime dateTime, List<Staff> staffs);
            
    }
}
