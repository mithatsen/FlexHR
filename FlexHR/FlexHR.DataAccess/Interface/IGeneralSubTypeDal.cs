﻿using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Interface
{
    public interface IGeneralSubTypeDal : IGenericDal<GeneralSubType>
    {
        List<GeneralSubType> GetGeneralSubTypeByGeneralTypeId(int generalTypeId);
        string GetDescriptionByGeneralSubTypeId(int generalSubTypeId);
    }
}
