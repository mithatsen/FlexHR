using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.DashboardDtos
{
   public  class ListStaffPublicDayOnDashboardDto
    {
        public int PublicHolidayId { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }

        public bool IsHalfDay { get; set; }
    }
}
