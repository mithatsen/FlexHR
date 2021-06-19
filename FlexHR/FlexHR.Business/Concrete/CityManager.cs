using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    
    public class CityManager : GenericManager<City>, ICityService
    {
        private readonly ICityDal _cityDal;

        public CityManager(ICityDal cityDal) : base(cityDal)
        {
            _cityDal = cityDal;
        }

        public List<City> GetCityListByCountryId(int id)
        {
            return _cityDal.GetCityListByCountryId(id);
        }
    }
}
