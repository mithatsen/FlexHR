using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;

namespace FlexHR.Business.Concrete
{
    public class UserRoleManager : GenericManager<UserRole>, IUserRoleService
    {
        public UserRoleManager(IUserRoleDal userRoleDal) : base(userRoleDal)
        {

        }
    }
  
}
