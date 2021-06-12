﻿using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace FlexHR.Business.Concrete
{
    public class StaffGeneralSubTypeManager : GenericManager<StaffGeneralSubType>, IStaffGeneralSubTypeService
    {
        private readonly IStaffGeneralSubTypeDal _generalSubTypeDal;
        public StaffGeneralSubTypeManager(IStaffGeneralSubTypeDal generalSubTypeDal) : base(generalSubTypeDal)
        {
            _generalSubTypeDal = generalSubTypeDal;
        }

        public List<StaffGeneralSubType> GetByStaffId(int id)
        {
            return _generalSubTypeDal.GetByStaffId(id);
        }


       
        public NameValueCollection GetGeneralSubTypeByStaffGeneralSubTypeList(List<StaffGeneralSubType> subTypes)
        {
            return _generalSubTypeDal.GetGeneralSubTypeByStaffGeneralSubTypeList(subTypes);
        }
    }
}
