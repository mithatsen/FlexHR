using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.ReceiptDtos
{
    public class ListReceiptDto
    {
        public string Name { get; set; }
        public decimal Vat { get; set; }
        public decimal Amount { get; set; }
        public string FileFullPath { get; set; }
        public string FileName { get; set; }
        public bool IsActive { get; set; }
        public int StaffPaymentId { get; set; }
    }
}
