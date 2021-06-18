using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class Staff : ITable
    {
        public int StaffId { get; set; }
        public string NameSurname { get; set; }
        public string EmailJob { get; set; }
        public string EmailPersonal { get; set; }
        public string PhoneJob { get; set; }
        public string PhonePersonal { get; set; }
        public DateTime JobJoinDate { get; set; }
        public DateTime? JobFinishDate { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool WillUseSystem { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<StaffCareer> StaffCareer { get; set; }
        public virtual ICollection<StaffDebit> StaffDebit { get; set; }
        public virtual ICollection<StaffFile> StaffFile { get; set; }
        public virtual ICollection<StaffGeneralSubType> StaffGeneralSubType { get; set; }
        public virtual ICollection<StaffLeave> StaffLeave { get; set; }
        public virtual ICollection<StaffOtherInfo> StaffOtherInfo { get; set; }
        public virtual ICollection<StaffPayment> StaffPayment { get; set; }
        public virtual ICollection<StaffPersonelInfo> StaffPersonelInfo { get; set; }
        public virtual ICollection<StaffSalary> StaffSalary { get; set; }
        public virtual ICollection<StaffShift> StaffShift { get; set; }
        public virtual ICollection<StaffRole> StaffRole { get; set; }
    }
}
