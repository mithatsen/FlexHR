using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.ViewModels
{
    public class GetMonthlyTakePaymentViewModel
    {
        public string PaymentType { get; set; }
        public string CurrencyType { get; set; }
        public decimal InstallmentAmount { get; set; }
    }
}
