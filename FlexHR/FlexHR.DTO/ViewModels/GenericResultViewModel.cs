using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.ViewModels
{
    public class GenericResultViewModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string SubMessage { get; set; }
        public string Type { get; set; }
        public string Param { get; set; }
    }
}
