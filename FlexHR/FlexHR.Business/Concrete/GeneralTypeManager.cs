using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class GeneralTypeManager : GenericManager<GeneralType>, IGeneralTypeService
    {
        public GeneralTypeManager(IGeneralTypeDal emailHistoryDal) : base(emailHistoryDal)
        {

        }
    }
}
