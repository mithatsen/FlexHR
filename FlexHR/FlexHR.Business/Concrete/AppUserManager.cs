using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class AppUserManager : GenericManager<AppUser>, IAppUserService
    {
        private readonly IAppUserDal _appUserDal;

   
        public AppUserManager(IAppUserDal appUserDal) : base(appUserDal)
        {
            _appUserDal = appUserDal;
        }

        public List<AppRole> GetAppRolesByStaffId(int id)
        {
            return _appUserDal.GetAppRolesByStaffId(id);
        }
    }
    
}
