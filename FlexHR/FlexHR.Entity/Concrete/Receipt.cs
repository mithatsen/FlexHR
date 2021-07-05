using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Entity.Concrete
{
    public class Receipt : ITable
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
        public decimal Vat { get; set; }
        public decimal Amount { get; set; }
        public string FileName { get; set; }
        public string FileFullPath { get; set; }
        public bool IsActive { get; set; }
        public int StaffPaymentId { get; set; }
        public virtual StaffPayment StaffPayment { get; set; }
    }
}
