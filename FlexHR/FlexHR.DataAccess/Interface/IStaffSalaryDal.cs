using FlexHR.DTO.Dtos.StaffSalaryDtos;
using FlexHR.DTO.ViewModels;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Interface
{
    public interface IStaffSalaryDal : IGenericDal<StaffSalary>
    {
        List<StaffTrackingMothlySalaryHelper> GetStaffSalaryMonthly(DateTime dateTime);
    }
}
