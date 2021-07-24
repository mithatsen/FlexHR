using FlexHR.DTO.Dtos.StaffLeaveDtos;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.ViewModels
{
    public class ListLeaveViewModel
    {
        public List<ListStaffLeaveDto> UpcomingLeaves { get; set; }
        public List<ListStaffLeaveDto> PendingApprovalLeaves { get; set; }
        public List<ListStaffLeaveDto> ApprovedLeaves { get; set; }
        public List<ListStaffLeaveDto> RejectedLeaves { get; set; }
    }
}
