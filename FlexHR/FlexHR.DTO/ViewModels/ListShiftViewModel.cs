using FlexHR.DTO.Dtos.StaffShiftDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.ViewModels
{
    public class ListShiftViewModel
    {
        public List<ListStaffShiftWithUserActiveInfo> ApprovedShift { get; set; }
        public List<ListStaffShiftWithUserActiveInfo> RejectedShift { get; set; }
        public List<ListStaffShiftWithUserActiveInfo> PendingApprovalLeaves { get; set; }
    }
}
