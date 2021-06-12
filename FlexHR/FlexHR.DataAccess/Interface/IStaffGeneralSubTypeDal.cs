using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace FlexHR.DataAccess.Interface
{
    public interface IStaffGeneralSubTypeDal : IGenericDal<StaffGeneralSubType>
    {
        List<StaffGeneralSubType> GetByStaffId(int id);

        NameValueCollection GetGeneralSubTypeByStaffGeneralSubTypeList(List<StaffGeneralSubType> subTypes);
    }
}
