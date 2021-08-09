using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Interface
{
    public interface IStaffService : IGenericService<Staff>
    {
       // int GetStaffIdByUserName(string userName);
        List<Staff> GetStaffBySearchString(string search);
    }
}
