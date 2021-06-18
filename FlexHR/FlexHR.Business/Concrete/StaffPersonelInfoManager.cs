using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;

namespace FlexHR.Business.Concrete
{
    public class StaffPersonelInfoManager : GenericManager<StaffPersonelInfo>, IStaffPersonelInfoService
    {
        private readonly IStaffPersonelInfoDal _staffPersonelInfoDal;
        public StaffPersonelInfoManager(IStaffPersonelInfoDal staffPersonelInfoDal) : base(staffPersonelInfoDal)
        {
            _staffPersonelInfoDal = staffPersonelInfoDal;
        }

        public StaffPersonelInfo GetPersonelInfoByStaffId(int id)
        {
            return _staffPersonelInfoDal.GetPersonelInfoByStaffId(id);
        }
    }
  
}
