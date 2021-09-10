using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.DTO.Dtos.StaffTracking;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class StaffManager : GenericManager<Staff>, IStaffService
    {
        private readonly IStaffDal _staffDal;
        public StaffManager(IStaffDal staffDal) : base(staffDal)
        {
            _staffDal = staffDal;
        }

        public List<Staff> GetStaffBySearchString(string search)
        {
            return _staffDal.GetStaffBySearchString(search);
        }

        public List<ListStaffTimeKeepingDto> GetStaffTimeKeepingMonthly(DateTime dateTime, List<Staff> staffs)
        {
            return _staffDal.GetStaffTimeKeepingMonthly(dateTime,staffs);
        }

        //public int GetStaffIdByUserName(string userName)
        //{
        //    return _staffDal.GetStaffIdByUserName(userName);
        //}
    }
}
