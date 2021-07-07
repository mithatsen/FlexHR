using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.ReceiptDtos
{
    public class AddReceiptDto
    {
        public string Name { get; set; }
        public decimal Vat { get; set; }
        public decimal ReceiptAmount { get; set; }
        
    
        public bool IsActive { get; set; }
        public int StaffPaymentId { get; set; }
    }
}
