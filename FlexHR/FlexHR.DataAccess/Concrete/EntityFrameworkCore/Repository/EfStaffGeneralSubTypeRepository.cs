using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using Microsoft.EntityFrameworkCore;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfStaffGeneralSubTypeRepository : EfGenericRepository<StaffGeneralSubType>, IStaffGeneralSubTypeDal
    {
        private readonly FlexHRContext _context;
        public EfStaffGeneralSubTypeRepository(FlexHRContext context) : base(context)
        {
            _context = context;
        }

        public List<StaffGeneralSubType> GetByStaffId(int id)
        {
            return _context.StaffGeneralSubType.Where(x => x.StaffId == id).ToList();
        }


        public NameValueCollection GetGeneralSubTypeByStaffGeneralSubTypeList(List<StaffGeneralSubType> subTypes)
        {
            NameValueCollection generalTypeArray =new NameValueCollection();
            for (int i = 0; i < subTypes.Count; i++)
            {
                var generalTypeId = subTypes[i].GeneralSubType.GeneralTypeId;
                var generalSubTypeDesc = subTypes[i].GeneralSubType.Description;
                generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
            }

            return generalTypeArray;

        }
    }

}
