using FlexHR.DTO.Dtos.AppUserDtos;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffGeneralDtos
{
    public class ListStaffGeneralDto
    {
        public int StaffId { get; set; }
        public string NameSurname { get; set; }
        public string EmailJob { get; set; }
        public string EmailPersonal { get; set; }
        public string PhoneJob { get; set; }
        public int PersonalNo { get; set; }
        public string PhonePersonal { get; set; }
        public DateTime JobJoinDate { get; set; }
        public DateTime? JobFinishDate { get; set; }
        public int ContractTypeGeneralSubTypeId { get; set; }
        public bool IsActive { get; set; } = true; 



    }
}
