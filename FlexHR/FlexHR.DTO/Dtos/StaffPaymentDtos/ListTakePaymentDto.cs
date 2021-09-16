using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffPaymentDtos
{
    public class ListTakePaymentDto
    {
        public int Id { get; set; }
        public int StaffPaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public int CurrencyGeneralSubTypeId { get; set; }
        public string CurrencyType { get; set; }
        public decimal InstallmentAmount { get; set; }
        public bool IsPaid { get; set; }
        public bool IsActive { get; set; }
    }
}
