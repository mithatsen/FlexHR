using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.ViewModels
{
    public class AddAuthorizeTypeList
    {
        public List<int> Users { get; set; }
        public List<int> Roles { get; set; }
        public List<int> AuthorizeTypes { get; set; }

    }
}
