using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Interface
{
    public interface IGeneralSubTypeService : IGenericService<GeneralSubType>
    {
        List<GeneralSubType> GetGeneralSubTypeByGeneralTypeId(int generalTypeId);
        string GetDescriptionByGeneralSubTypeId(int generalSubTypeId);
    }
}
