using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class EventManager : GenericManager<Event>, IEventService
    {
        public EventManager(IEventDal emailHistoryDal) : base(emailHistoryDal)
        {

        }
    }
}
