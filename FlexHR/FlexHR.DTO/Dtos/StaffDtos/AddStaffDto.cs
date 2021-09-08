using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffDtos
{
    public class AddStaffDto
    {
        public string NameSurname { get; set; }
        public string EmailJob { get; set; }
        public string EmailPersonal { get; set; }
        public string PhoneJob { get; set; }
        public string PhonePersonal { get; set; }
        public DateTime JobJoinDate { get; set; }
        public DateTime? JobFinishDate { get; set; }
        public int ContractTypeGeneralSubTypeId { get; set; }
                   
        public int RoleId { get; set; }
        public int PersonalNo { get; set; }
        public bool WillUseSystem { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<int> PageRoles { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
