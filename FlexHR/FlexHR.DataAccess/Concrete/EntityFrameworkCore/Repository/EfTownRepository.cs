using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfTownRepository : EfGenericRepository<Town>, ITownDal
    {
        private readonly FlexHRContext _context;
        public EfTownRepository(FlexHRContext context) : base(context)
        {
            _context = context;
        }
     

        public List<Town> GetTownListByCityId(int id)
        {
            return _context.Town.Where(I => I.CityId == id).Select(I => new Town
            {
                TownId = I.TownId,
                Name=I.Name,
            }).ToList();
        }
    }
}