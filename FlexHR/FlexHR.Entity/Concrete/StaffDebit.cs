﻿using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class StaffDebit : ITable
    {
        public int StaffDebitId { get; set; }
        public int StaffId { get; set; }
        public int DebitGeneralSubTypeId { get; set; }
        public string SerialNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
