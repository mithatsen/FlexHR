using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class StaffPersonelInfo : ITable
    {
        public int StaffPersonelInfoId { get; set; }
        public int StaffId { get; set; }
        public DateTime? BirthDate { get; set; }
        public string IdNumber { get; set; }
        public int? MaritalStatusGeneralSubTypeId { get; set; } = 1;
        public int? GenderGeneralSubTypeId { get; set; } = 1;
        public int? DegreeOfDisabilityGeneralSubTypeId { get; set; } = 1;
        public int? BloodGroupGeneralSubTypeId { get; set; } = 1;
        public int? EducationStatusGeneralSubTypeId { get; set; } = 1;
        public int? EducationLevelGeneralSubTypeId { get; set; } = 1;
        public bool IsActive { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
