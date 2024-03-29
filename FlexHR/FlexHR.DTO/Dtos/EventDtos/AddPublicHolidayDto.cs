﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.EventDtos
{
    public class AddPublicHolidayDto
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }

        public bool IsActive { get; set; }
        public bool IsHalfDay { get; set; }
    }
}
