using FlexHR.DTO.Dtos.ReceiptDtos;
using FlexHR.DTO.Dtos.StaffPaymentDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.ViewModels
{
    public class AddStaffPaymentWithAjaxViewModel
    {
        public AddStaffPaymentDto model { get; set; }
        public int id { get; set; }
        public List<AddReceiptDto> receipts { get; set; }       
           
    }
}
