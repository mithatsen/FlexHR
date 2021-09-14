using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
   
    public class TakePaymentManager : GenericManager<TakePayment>, ITakePaymentService
    {
        public TakePaymentManager(ITakePaymentDal takePaymentManagerDal) : base(takePaymentManagerDal)
        {

        }
    }
}
