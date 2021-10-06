using FlexHR.DTO.Dtos.StaffTracking;
using FlexHR.DTO.ViewModels;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Interface
{
    public interface IStaffService : IGenericService<Staff>
    {
       // int GetStaffIdByUserName(string userName);
        List<Staff> GetStaffBySearchString(string search);
        List<ListStaffTimeKeepingDto> GetStaffTimeKeepingMonthly(DateTime dateTime, List<Staff> staffs);
        int GetStaffReportDayMonthly(DateTime dateTime, int cardNo);

        StaffSalaryMonthlyHelper GetAbsenceInformationMonthly(DateTime dateTime, int staffId);
    } 
}
