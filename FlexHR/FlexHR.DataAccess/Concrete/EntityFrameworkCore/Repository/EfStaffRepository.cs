using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfStaffRepository : EfGenericRepository<Staff>, IStaffDal
    {
        private readonly FlexHRContext _context;
        public EfStaffRepository(FlexHRContext context) : base(context)
        {
            _context = context;
        }

        //public Staff GetAllTables(int id)
        //{
        //    return _context.Staff.Include(x => x.StaffGeneralSubType).ThenInclude(x => x.GeneralSubType).ThenInclude(x => x.GeneralType).Where(p=>p.StaffId==id).FirstOrDefault();
        //}

        public List<Staff> GetStaffBySearchString(string search)
        {
            return _context.Staff.Include(p=>p.StaffPersonelInfo).Where(p => p.IsActive==true && (p.UserName.Contains(search)|| p.NameSurname.Contains(search)|| p.StaffPersonelInfo.FirstOrDefault().IdNumber.Contains(search))).ToList();
        }

        public int GetStaffIdByUserName(string userName)
        {
            return _context.Staff.Where(p => p.UserName == userName).FirstOrDefault().StaffId;
        }
    }
}
