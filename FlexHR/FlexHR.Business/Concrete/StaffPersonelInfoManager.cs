using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;

namespace FlexHR.Business.Concrete
{
    public class StaffPersonelInfoManager : GenericManager<StaffPersonelInfo>, IStaffPersonelInfoService
    {
        public StaffPersonelInfoManager(IStaffPersonelInfoDal staffPersonelInfoDal) : base(staffPersonelInfoDal)
        {

        }
    }
  
}
