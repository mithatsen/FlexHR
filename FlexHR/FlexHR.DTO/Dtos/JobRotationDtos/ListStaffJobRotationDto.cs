﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DTO.Dtos.JobRotationDtos
{
    public class ListStaffJobRotationDto
    {
        public int StaffId { get; set; }
        public int? JobRotationId { get; set; }
        public string NameSurname { get; set; }
        public string DepartmentName { get; set; }
        public int PersonalNo { get; set; }
    }
}