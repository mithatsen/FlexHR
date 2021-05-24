using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class StaffPayment : ITable
    {
        public int StaffPaymentId { get; set; }
        public int StaffId { get; set; }
        public int GeneralStatusGeneralSubTypeId { get; set; }
        public int PaymentTypeGeneralSubTypeId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsPaid { get; set; }
        public bool IsSentForApproval { get; set; }
        public bool IsMailSentToStaff { get; set; }
        public bool IsActive { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
