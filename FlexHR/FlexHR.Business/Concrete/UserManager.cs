﻿using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;

namespace FlexHR.Business.Concrete
{
    public class UserManager : GenericManager<User>, IUserService
    {
        public UserManager(IUserDal userDal) : base(userDal)
        {

        }
    }
  
}
