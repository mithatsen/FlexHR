using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class UserManager : GenericManager<User>, IUserService
    {
        public UserManager(IUserDal userDal) : base(userDal)
        {

        }
    }
  
}
