using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class JobRotationHistoryManager : GenericManager<JobRotationHistory>, IJobRotationHistoryService
    {
        public JobRotationHistoryManager(IJobRotationHistoryDal jobRotationHistoryDal) : base(jobRotationHistoryDal)
        {

        }
    }
}
