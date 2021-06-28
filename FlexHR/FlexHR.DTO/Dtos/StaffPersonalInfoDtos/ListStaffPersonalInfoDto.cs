using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffPersonalInfoDtos
{
    public class ListStaffPersonalInfoDto
    {
        public int StaffId { get; set; }
        public DateTime? BirthDate { get; set; }
        public string IdNumber { get; set; }
        public int? MaritalStatusGeneralSubTypeId { get; set; }
        public int? GenderGeneralSubTypeId { get; set; }
        public int? DegreeOfDisabilityGeneralSubTypeId { get; set; }
        public int? BloodGroupGeneralSubTypeId { get; set; }
        public int? EducationStatusGeneralSubTypeId { get; set; }
        public int? EducationLevelGeneralSubTypeId { get; set; }
        public bool IsActive { get; set; }
    }
}
