using FlexHR.DTO.Dtos.StaffFileDtos;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.StaffDtos
{
    public class ListStaffDto
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
        public string PictureUrl { get; set; }
        public string? Title { get; set; }
        public string? DepartmantName { get; set; }
        public string? BranchName { get; set; }
        public List<StaffFile> StaffFile { get; set; }
       


    }
}
