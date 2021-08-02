using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.GeneralSubTypeDtos
{
    public class ListGeneralSubTypeDto
    {
        public int GeneralSubTypeId { get; set; }
        public int GeneralTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

    }
}
