using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class StaffOtherInfoManager : GenericManager<StaffOtherInfo>, IStaffOtherInfoService
    {
        private readonly IStaffOtherInfoDal _staffOtherInfoDal;

        public StaffOtherInfoManager(IStaffOtherInfoDal staffOtherInfoDal) : base(staffOtherInfoDal)
        {
            _staffOtherInfoDal = staffOtherInfoDal;
        }

        public StaffOtherInfo GetOtherInfoByStaffId(int id)
        {
            return _staffOtherInfoDal.GetOtherInfoByStaffId(id);
        }
    }
}
