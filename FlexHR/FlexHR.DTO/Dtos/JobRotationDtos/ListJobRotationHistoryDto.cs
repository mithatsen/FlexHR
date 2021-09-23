using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.JobRotationDtos
{
    public class ListJobRotationHistoryDto
    {
        public int Id { get; set; }
        public int JobRotationId { get; set; }
        public int StaffId { get; set; }
        public DateTime JobRotationDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
