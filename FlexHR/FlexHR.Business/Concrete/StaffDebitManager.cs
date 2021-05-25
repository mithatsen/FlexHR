using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class StaffDebitManager : GenericManager<StaffDebit>, IStaffDebitService
    {
        public StaffDebitManager(IStaffDebitDal emailHistoryDal) : base(emailHistoryDal)
        {

        }
    }
}
