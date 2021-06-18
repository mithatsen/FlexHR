using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class GeneralSubTypeManager : GenericManager<GeneralSubType>, IGeneralSubTypeService
    {
        private readonly IGeneralSubTypeDal _generalSubTypeDal;
        public GeneralSubTypeManager(IGeneralSubTypeDal generalSubTypeDal) : base(generalSubTypeDal)
        {
            _generalSubTypeDal = generalSubTypeDal;

        }

        public string GetDescriptionByGeneralSubTypeId(int generalSubTypeId)
        {
            return _generalSubTypeDal.GetDescriptionByGeneralSubTypeId(generalSubTypeId);
        }

        public List<GeneralSubType> GetGeneralSubTypeByGeneralTypeId(int generalTypeId)
        {
            return _generalSubTypeDal.GetGeneralSubTypeByGeneralTypeId(generalTypeId);
        }

  
    }
}
