using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;

namespace FlexHR.Business.Concrete
{
    public class StaffRoleManager : GenericManager<StaffRole>, IStaffRoleService
    {
        private readonly IStaffRoleDal _userRoleDal;
        public StaffRoleManager(IStaffRoleDal userRoleDal) : base(userRoleDal)
        {
            _userRoleDal = userRoleDal;
        }

        public StaffRole GetUserRoleByStaffId(int id)
        {
            return _userRoleDal.GetUserRoleByStaffId(id);
        }
    }
  
}
