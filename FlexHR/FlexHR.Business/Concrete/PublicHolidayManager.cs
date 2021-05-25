using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class PublicHolidayManager : GenericManager<PublicHoliday>, IPublicHolidayService
    {
        public PublicHolidayManager(IPublicHolidayDal emailHistoryDal) : base(emailHistoryDal)
        {

        }
    }
}
