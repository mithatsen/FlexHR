using FlexHR.DTO.Dtos.StaffLeaveDtos;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.ViewModels
{
    public class ListLeaveViewModel
    {
        public List<ListStaffLeaveWithUserActiveInfoDto> PendingApprovalLeaves { get; set; }
        public List<ListStaffLeaveWithUserActiveInfoDto> ApprovedLeaves { get; set; }
        public List<ListStaffLeaveWithUserActiveInfoDto> RejectedLeaves { get; set; }
    }
}
