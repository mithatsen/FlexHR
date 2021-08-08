using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.DashboardDtos
{
    public class ListStaffEventOnDashboardDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
