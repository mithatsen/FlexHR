using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Interface
{
    public interface IStaffService : IGenericService<Staff>
    {
        Staff GetAllTables(int id);
    }
}
