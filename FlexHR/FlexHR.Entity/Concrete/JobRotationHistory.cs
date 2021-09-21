using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class JobRotationHistory : ITable
    {
        public int Id { get; set; }
        public int JobRotationId { get; set; }
        public int StaffId { get; set; }
        public DateTime JobRotationDate { get; set; }
        public bool IsActive { get; set; }
        //public List<Staff> Staffs { get; set; }
        public JobRotation JobRotations { get; set; }
    }
}
