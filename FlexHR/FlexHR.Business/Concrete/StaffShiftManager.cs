using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;

namespace FlexHR.Business.Concrete
{
    public class StaffShiftManager : GenericManager<StaffShift>, IStaffShiftService
    {
        public StaffShiftManager(IStaffShiftDal staffShiftManagerDal) : base(staffShiftManagerDal)
        {

        }
    }
  
}
