using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Interface
{
    public interface IStaffGeneralSubTypeDal : IGenericDal<StaffGeneralSubType>
    {
        List<StaffGeneralSubType> GetByStaffId(int id);

        System.Collections.Specialized.NameValueCollection GetGeneralSubTypeByStaffGeneralSubTypeList(List<StaffGeneralSubType> subTypes);
    }
}
