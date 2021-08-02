using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.GeneralSubTypeDtos
{
    public class AddGeneralSubTypeDto
    {
        public int GeneralTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
