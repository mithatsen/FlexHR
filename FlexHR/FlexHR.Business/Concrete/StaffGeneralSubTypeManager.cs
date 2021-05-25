using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class StaffGeneralSubTypeManager : GenericManager<StaffGeneralSubType>, IStaffGeneralSubTypeService
    {
        public StaffGeneralSubTypeManager(IStaffGeneralSubTypeDal generalSubTypeDal) : base(generalSubTypeDal)
        {

        }
    }
}
