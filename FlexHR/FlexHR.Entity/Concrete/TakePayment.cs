using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    //ödemeyi geri almak avans ve icra 
    public class TakePayment:ITable
    {
        public int Id { get; set; }
        public int StaffPaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal  InstallmentAmount { get; set; }
        public bool IsPaid { get; set; }
        public bool IsActive { get; set; }
    }
}
