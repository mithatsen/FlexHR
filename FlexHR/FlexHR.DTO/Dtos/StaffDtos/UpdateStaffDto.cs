﻿using FlexHR.DTO.Dtos.StaffCareerDtos;
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
        public string DepartmantName { get; set; }
        public string Superscription { get; set; }
        public string ContractType { get; set; }
        public string StaffRole { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string ModeOfOperation { get; set; }
        public int CountryId { get; set; } = 3;
        public int CityId { get; set; } = 3;
        public int TownId { get; set; } = 3;

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
    }
}
