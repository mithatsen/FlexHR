﻿using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class CountryManager : GenericManager<Country>, ICountryService
    {
        public CountryManager(ICountryDal countryDal) : base(countryDal)
        {

        }
    }
}
