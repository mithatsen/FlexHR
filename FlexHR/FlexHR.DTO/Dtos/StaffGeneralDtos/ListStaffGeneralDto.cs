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
        public string PhonePersonal { get; set; }
        public DateTime JobJoinDate { get; set; }
        public DateTime? JobFinishDate { get; set; }
        public int ContractTypeId { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }



    }
}
