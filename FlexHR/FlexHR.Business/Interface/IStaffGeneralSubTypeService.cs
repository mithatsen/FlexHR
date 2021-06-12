using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Interface
{
    public interface IStaffGeneralSubTypeService : IGenericService<StaffGeneralSubType>
    {
        List<StaffGeneralSubType> GetByStaffId(int id);
        System.Collections.Specialized.NameValueCollection GetGeneralSubTypeByStaffGeneralSubTypeList(List<StaffGeneralSubType> subTypes);
    }
}
