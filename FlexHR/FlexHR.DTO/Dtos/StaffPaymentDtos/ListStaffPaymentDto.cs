using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FlexHR.DTO.Dtos.StaffPaymentDtos
{
    public class ListStaffPaymentDto
    {
        public int StaffPaymentId { get; set; }
        public int StaffId { get; set; }
        public string NameSurname { get; set; }
        public int GeneralStatusGeneralSubTypeId { get; set; }
        public int PaymentTypeGeneralSubTypeId { get; set; }
        public int? FeeTypeGeneralSubTypeId { get; set; }

        public int CurrencyGeneralSubTypeId { get; set; }
        public string PaymentType { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }
        public string CurrencyType { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsPaid { get; set; }
        public bool IsSentForApproval { get; set; }
        public bool IsMailSentToStaff { get; set; }
        public bool IsActive { get; set; }
        public int? Installment { get; set; }


        public virtual ICollection<Receipt> Receipts { get; set; }
        public virtual List<Receipt> Receipts2 { get { return Receipts!=null? Receipts.ToList() : null; } set { } }
    }
}
