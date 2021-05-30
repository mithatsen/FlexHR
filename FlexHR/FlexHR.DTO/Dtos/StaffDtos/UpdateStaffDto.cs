using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffDtos
{
    public class UpdateStaffDto
    {
        public int StaffId { get; set; }
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
        public List<GeneralSubType> ContractTypeList { get; set; } // bu dropdown için
        public List<Role> Roles { get; set; } // bu dropdown için
        public virtual ICollection<StaffGeneralSubType> StaffGeneralSubType { get; set; }
    }
}
