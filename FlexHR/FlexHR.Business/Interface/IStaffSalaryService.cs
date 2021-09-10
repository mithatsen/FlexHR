using FlexHR.DTO.Dtos.StaffSalaryDtos;
using FlexHR.DTO.ViewModels;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Interface
{
    public interface IStaffSalaryService : IGenericService<StaffSalary>
    {
        List<StaffTrackingMothlySalaryHelper> GetStaffSalaryMonthly(DateTime dateTime);
    }
}
