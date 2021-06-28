using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffOtherInfoDtos
{
    public class ListStaffOtherInfoDto
    {
        public int StaffOtherInfoId { get; set; }
        public int StaffId { get; set; }
        public int? TownId { get; set; }
        public string Adress { get; set; }
        public string HomePhoneNumber { get; set; }
        public string PostalCode { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public int? AccountTypeGeneralSubTypeId { get; set; }
        public string Iban { get; set; }
        public string CallPersonNameSurname { get; set; }
        public string CallPersonProximityDegree { get; set; }
        public string CallPersonPhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public TownHelper TownHelper { get; set; }
    }
}
