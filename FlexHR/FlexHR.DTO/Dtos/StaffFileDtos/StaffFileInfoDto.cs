using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffFileDtos
{
    public class StaffFileInfoDto
    {
        public FileHelper IdentityBook { get; set; }
        public FileHelper DrivingLicence { get; set; }
        public FileHelper SchoolLeavingCertificate { get; set; }
        public FileHelper ResidenceCertificate{ get; set; }
        public FileHelper CriminalCertificate { get; set; }
        public FileHelper Certificates { get; set; }
       
    }
}
