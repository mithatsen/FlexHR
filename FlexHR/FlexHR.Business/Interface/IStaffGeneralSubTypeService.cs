using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace FlexHR.Business.Interface
{
    public interface IStaffGeneralSubTypeService : IGenericService<StaffGeneralSubType>
    {
        List<StaffGeneralSubType> GetByStaffId(int id);
      
        
        /// <summary>
        /// Bu methotta gelen personel general subtype listemizdeki verileri general type id ve general sub type tablosunun descriptionunu alarak list içinde key value olarak döndürüyor.
        /// </summary>
        /// <param name="subTypes"></param>
        /// <returns></returns>
        NameValueCollection GetGeneralSubTypeByStaffGeneralSubTypeList(List<StaffGeneralSubType> subTypes);
    }
}
