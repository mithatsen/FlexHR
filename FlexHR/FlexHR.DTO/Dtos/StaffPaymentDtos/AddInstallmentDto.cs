using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffPaymentDtos
{
    public class AddInstallmentDto
    {
        public int StaffPaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal InstallmentAmount { get; set; }
        public bool IsPaid { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
