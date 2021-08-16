using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.ViewModels
{
    public class AddExcelViewModel
    {
        public int CategoryId { get; set; }
        public DateTime Date { get; set; }
        public IFormFile file { get; set; }
    }
}
