using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Interface
{
    public interface IStaffCareerDal : IGenericDal<StaffCareer>
    {
        List<StaffCareer> GetAllTableByStaffId(int id);
    }
}
