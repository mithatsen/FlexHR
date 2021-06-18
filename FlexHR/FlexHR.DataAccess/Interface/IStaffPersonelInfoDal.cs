using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Interface
{
    public interface IStaffPersonelInfoDal : IGenericDal<StaffPersonelInfo>
    {
        StaffPersonelInfo GetPersonelInfoByStaffId(int id);
    }
}
