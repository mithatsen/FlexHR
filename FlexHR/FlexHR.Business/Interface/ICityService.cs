﻿using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Interface
{
    public interface ICityService:IGenericService<City>
    {
        public List<City> GetCityListByCountryId(int id);
    }
}
