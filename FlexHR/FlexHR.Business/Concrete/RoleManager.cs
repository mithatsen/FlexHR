using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class RoleManager : GenericManager<Role>, IRoleService
    {
        public RoleManager(IRoleDal emailHistoryDal) : base(emailHistoryDal)
        {

        }
    }
}
