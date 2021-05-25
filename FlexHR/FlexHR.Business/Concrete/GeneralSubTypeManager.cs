using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class GeneralSubTypeManager : GenericManager<GeneralSubType>, IGeneralSubTypeService
    {
        public GeneralSubTypeManager(IGeneralSubTypeDal emailHistoryDal) : base(emailHistoryDal)
        {

        }
    }
}
