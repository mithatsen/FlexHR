using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffPaymentDtos
{
    public class AddStaffPaymentDto
    {
        public int StaffId { get; set; }
        public int GeneralStatusGeneralSubTypeId { get; set; } = 96;
        public int PaymentTypeGeneralSubTypeId { get; set; }
        public int CurrencyGeneralSubTypeId { get; set; }
        public int? FeeTypeGeneralSubTypeId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool IsPaid { get; set; } 
        public bool IsSentForApproval { get; set; }
        public bool IsMailSentToStaff { get; set; }
        public bool IsActive { get; set; } = true;
        public int? Installment { get; set; }
        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
