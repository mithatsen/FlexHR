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
        public List<ListStaffBirthOnDashboardDto> BirtDates { get; set; }
        public List<ListStaffEventOnDashboardDto> Events { get; set; }
        public List<ListStaffPublicDayOnDashboardDto> PublicDays { get; set; }
        public List<ListStaffUpcomingLeaveOnDashboardDto> UpcomingLeaves { get; set; }
    }
}
