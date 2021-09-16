using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffPaymentDtos
{
    public class ListPaymentForTakePaymentDto
    {

        public int StaffId { get; set; }
        public int StaffPaymentId { get; set; }
        public string NameSurname { get; set; }
        public int PaymentTypeGeneralSubTypeId { get; set; }
        public string PaymentType { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Amount { get; set; }
        public int? Installment { get; set; }
    }
}
