using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.DTO.Dtos.StaffSalaryDtos;
using FlexHR.DTO.ViewModels;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;

namespace FlexHR.Business.Concrete
{
    public class StaffSalaryManager : GenericManager<StaffSalary>, IStaffSalaryService
    {
        private readonly IStaffSalaryDal _staffSalaryDal;
        public StaffSalaryManager(IStaffSalaryDal staffSalaryDal) : base(staffSalaryDal)
        {
            _staffSalaryDal = staffSalaryDal;
        }

        public List<StaffTrackingMothlySalaryHelper> GetStaffSalaryMonthly(DateTime dateTime)
        {
            return _staffSalaryDal.GetStaffSalaryMonthly(dateTime);
        }
    }
  
}
