using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    
    public class EfTakePaymentRepository : EfGenericRepository<TakePayment>, ITakePaymentDal
    {
        public EfTakePaymentRepository(FlexHRContext context) : base(context)
        {

        }
    }
}
