using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.JobRotationDtos
{
    public class ListJobRotationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal ShiftTime { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
