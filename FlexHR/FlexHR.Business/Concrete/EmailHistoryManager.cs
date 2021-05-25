using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class EmailHistoryManager : GenericManager<EmailHistory>, IEmailHistoryService
    {
        public EmailHistoryManager(IEmailHistoryDal emailHistoryDal) : base(emailHistoryDal)
        {

        }
    }
}
