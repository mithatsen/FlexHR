using FlexHR.DTO.Dtos.StaffPaymentDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.ViewModels
{
    public class ListPaymentViewModel
    {
        public List<ListStaffPaymentDto> PendingApprovalPayments { get; set; }
        public List<ListStaffPaymentDto> ApprovedPayments { get; set; }
        public List<ListStaffPaymentDto> RejectedPayments { get; set; }
    }
}
