using FlexHR.DTO.Dtos.StaffPaymentDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.ViewModels
{
    public class ListPaymentViewModel
    {
        public List<ListStaffPaymentWithUserActiveInfoDto> PendingApprovalPayments { get; set; }
        public List<ListStaffPaymentWithUserActiveInfoDto> ApprovedPayments { get; set; }
        public List<ListStaffPaymentWithUserActiveInfoDto> RejectedPayments { get; set; }
    }
}
