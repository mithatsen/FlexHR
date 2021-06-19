using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfCityRepository : EfGenericRepository<City>, ICityDal
    {
        private readonly FlexHRContext _context;
        public EfCityRepository(FlexHRContext context):base(context)
        {
            _context = context;
        }
       
      
   
        public List<City> GetCityListByCountryId(int id)
        {
            return _context.City.Where(I => I.CountryId == id).Select(I => new City
            {
                CityId = I.CityId,
                Name = I.Name,
            }).ToList();
        }
    }
}
