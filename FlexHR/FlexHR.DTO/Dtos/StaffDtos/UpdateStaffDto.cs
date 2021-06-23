using FlexHR.DTO.Dtos.StaffCareerDtos;
using FlexHR.DTO.Dtos.StaffLeaveDtos;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffDtos
{
    public class UpdateStaffDto
    {
        public int StaffId { get; set; }
        public int ContractTypeId { get; set; }
        public int RoleId { get; set; }
        public string NameSurname { get; set; }
        public string EmailJob { get; set; }
        public string EmailPersonal { get; set; }
        public string PhoneJob { get; set; }
        public string PhonePersonal { get; set; }
        public DateTime JobJoinDate { get; set; }
        public DateTime? JobFinishDate { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int CountryId { get; set; } = -1;
        public int CityId { get; set; } = -1;
        public int TownId { get; set; } = -1;
        public TownHelper TownHelper { get; set; }
        public ListStaffCareerDto ActiveCareer { get; set; }
        public StaffPersonelInfo StaffPersonelInfo { get; set; } 
        public StaffOtherInfo StaffOtherInfo { get; set; } 
 

        public List<GeneralSubType> BloodGroupList { get; set; }
        public List<GeneralSubType> GenderList { get; set; }
        public List<GeneralSubType> EducationStatusList { get; set; }
        public List<GeneralSubType> EducationLevelList { get; set; }
        public List<GeneralSubType> DegreeOfDisabilityList { get; set; }
        public List<GeneralSubType> MaritialStatusList { get; set; }
        public List<GeneralSubType> AccountTypeList { get; set; }
        public List<GeneralSubType> ContractTypeList { get; set; } // bu dropdown için
  
        public List<Role> Roles { get; set; } // bu dropdown için
        public virtual ICollection<StaffGeneralSubType> StaffGeneralSubType { get; set; }
        public List<ListStaffCareerDto> ListStaffCareer { get; set; }
        public List<ListStaffLeaveDto> ListStaffLeave { get; set; }
    }
}
