using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.DashboardDtos
{
    public class ListStaffPaymentOnDashboardDto
    {
        public int StaffPaymentId { get; set; }
        public int StaffId { get; set; }
        public string NameSurname { get; set; }
        public int PaymentTypeGeneralSubTypeId { get; set; }
        public int GenderGeneralSubTypeId { get; set; }
        public int? FeeTypeGeneralSubTypeId { get; set; }
        public int CurrencyGeneralSubTypeId { get; set; }
        public string PaymentType { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }
        public string CurrencyType { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreationDate { get; set; }
        public int? Installment { get; set; }
        public virtual ICollection<Receipt> Receipts { get; set; }

    }
}
