using FlexHR.DTO.Dtos.StaffCareerDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffCardDtos
{
    public class StaffCardDto
    {
        public int? StaffFileId { get; set; } 
        public DateTime JobJoinDate { get; set; }
        public string NameSurname { get; set; }
        public string EmailJob { get; set; }
        public string PhoneJob { get; set; }
        //active career
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public string DepartmantName { get; set; }
        public string Title { get; set; }
        public string PictureUrl { get; set; }
    }
}
