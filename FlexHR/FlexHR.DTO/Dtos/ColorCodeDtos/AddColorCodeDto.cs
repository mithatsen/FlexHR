using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.ColorCodeDtos
{
    public class AddColorCodeDto
    {
        public string Color { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
