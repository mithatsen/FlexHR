using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfGeneralSubTypeRepository : EfGenericRepository<GeneralSubType>, IGeneralSubTypeDal
    {
        private readonly FlexHRContext _context;
        public EfGeneralSubTypeRepository(FlexHRContext context) : base(context)
        {
            _context = context;
        }

        public string GetDescriptionByGeneralSubTypeId(int generalSubTypeId)
        {
            var temp = _context.GeneralSubType.Where(x => x.GeneralSubTypeId == generalSubTypeId).FirstOrDefault();
            return temp.Description;
        }

        public List<GeneralSubType> GetGeneralSubTypeByGeneralTypeId(int generalTypeId)
        {
            return _context.GeneralSubType.Where(x => x.GeneralTypeId == generalTypeId && x.IsActive==true).ToList();         
        }
    }
}
