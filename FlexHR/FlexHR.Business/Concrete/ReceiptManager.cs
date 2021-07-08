using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class ReceiptManager : GenericManager<Receipt>, IReceiptService
    {
        private readonly IReceiptDal _receiptDal;

        public ReceiptManager(IReceiptDal receiptDal) : base(receiptDal)
        {
            _receiptDal = receiptDal;
        }

    }
}
