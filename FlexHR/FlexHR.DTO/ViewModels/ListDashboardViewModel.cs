using FlexHR.DTO.Dtos.DashboardDtos;
using FlexHR.DTO.Dtos.StaffShiftDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.ViewModels
{
    public class ListDashboardViewModel
    {
        public List<ListStaffShiftOnDashboardDto> StaffShifts { get; set; }
        public List<ListStaffLeaveOnDashboardDto> StaffLeaves { get; set; }
        public List<ListStaffPaymentOnDashboardDto> StaffPayments { get; set; }
    }
}
