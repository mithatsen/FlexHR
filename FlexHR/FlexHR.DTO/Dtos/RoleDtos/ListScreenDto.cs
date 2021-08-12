using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.RoleDtos
{
    public class ListScreenDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AuthorizeTypeGeneralSubTypeId { get; set; }
        public string AuthorizeType { get; set; }
        public bool IsActive { get; set; }
    }
}
