using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Interface
{
    public interface ICompanyService : IGenericService<Company>
    {
        string GetCompanyNameByCompanyId(int id);
    }
}
