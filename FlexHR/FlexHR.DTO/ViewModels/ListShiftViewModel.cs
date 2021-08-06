using FlexHR.DTO.Dtos.StaffShiftDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.ViewModels
{
    public class ListShiftViewModel
    {
        public List<ListStaffShiftDto> ApprovedShift { get; set; }
        public List<ListStaffShiftDto> RejectedShift { get; set; }
        public List<ListStaffShiftDto> PendingApprovalLeaves { get; set; }
    }
}
