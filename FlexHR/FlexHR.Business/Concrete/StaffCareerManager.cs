using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class StaffCareerManager : GenericManager<StaffCareer>, IStaffCareerService
    {
        public StaffCareerManager(IStaffCareerDal emailHistoryDal) : base(emailHistoryDal)
        {

        }
    }
}
