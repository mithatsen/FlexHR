using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.DTO.Dtos.StaffTracking;
using FlexHR.Entity.Concrete;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class StaffTrackingManager : IStaffTrackingService
    {
        private readonly IStaffTrackingDal _staffTrackingDal;

        public StaffTrackingManager(IStaffTrackingDal staffTrackingDal)
        {
            _staffTrackingDal = staffTrackingDal;
        }

        public List<ListStaffTimeKeepingDto> GetStaffTimeKeepingMonthly(DateTime dateTime, List<Staff> staffs)
        {
            return _staffTrackingDal.GetStaffTimeKeepingMonthly(dateTime, staffs);
        }
    }
}
