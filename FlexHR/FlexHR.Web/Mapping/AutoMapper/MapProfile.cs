using AutoMapper;
using FlexHR.DTO.Dtos.AppUserDtos;
using FlexHR.DTO.Dtos.CompanyBranchDtos;
using FlexHR.DTO.Dtos.CompanyDtos;
using FlexHR.DTO.Dtos.DashboardDtos;
using FlexHR.DTO.Dtos.EventDtos;
using FlexHR.DTO.Dtos.GeneralSubTypeDtos;
using FlexHR.DTO.Dtos.LeaveRuleDtos;
using FlexHR.DTO.Dtos.LeaveTypeDtos;
using FlexHR.DTO.Dtos.ReceiptDtos;
using FlexHR.DTO.Dtos.RoleDtos;
using FlexHR.DTO.Dtos.StaffCareerDtos;
using FlexHR.DTO.Dtos.StaffDebitDtos;
using FlexHR.DTO.Dtos.StaffDtos;
using FlexHR.DTO.Dtos.StaffGeneralDtos;
using FlexHR.DTO.Dtos.StaffLeaveDtos;
using FlexHR.DTO.Dtos.StaffOtherInfoDtos;
using FlexHR.DTO.Dtos.StaffPaymentDtos;
using FlexHR.DTO.Dtos.StaffPersonalInfoDtos;
using FlexHR.DTO.Dtos.StaffShiftDtos;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Mapping.AutoMapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            #region Staff
            CreateMap<ListStaffDto, Staff>().ReverseMap();
            CreateMap<AddStaffDto, Staff>().ReverseMap();
            CreateMap<UpdateStaffPersonalInfoDto, StaffPersonelInfo>().ReverseMap();
            CreateMap<ListStaffGeneralDto, Staff>().ReverseMap();
            CreateMap<UpdateStaffGeneralDto, Staff>().ReverseMap();
            #endregion
            #region StaffCareersta
            CreateMap<ListStaffCareerDto, StaffCareer>().ReverseMap();
            CreateMap<AddStaffCareerDto, StaffCareer>().ReverseMap();
            #endregion
            #region GeneralSubType
            CreateMap<ListGeneralSubTypeDto, GeneralSubType>().ReverseMap();
            CreateMap<AddGeneralSubTypeDto, GeneralSubType>().ReverseMap();
            #endregion 
            #region LeaveType
            CreateMap<ListLeaveTypeDto, LeaveType>().ReverseMap();
            CreateMap<AddLeaveTypeDto, LeaveType>().ReverseMap();
            #endregion
            #region StaffLeave
            CreateMap<AddStaffLeaveDto, StaffLeave>().ReverseMap();
            CreateMap<ListStaffLeaveDto, StaffLeave>().ReverseMap();
            CreateMap<StaffLeave, ListStaffLeaveDto>().ForMember(d => d.LeaveType, o => o.MapFrom(s => s.LeaveType.Name)).ForMember(d => d.NameSurname, o => o.MapFrom(s => s.Staff.NameSurname));
            CreateMap<StaffLeave, ListStaffLeaveOnDashboardDto>().ForMember(d => d.LeaveType, o => o.MapFrom(s => s.LeaveType.Name)).ForMember(d => d.NameSurname, o => o.MapFrom(s => s.Staff.NameSurname));
            CreateMap<StaffLeave, ListStaffUpcomingLeaveOnDashboardDto>().ForMember(d => d.LeaveType, o => o.MapFrom(s => s.LeaveType.Name)).ForMember(d => d.NameSurname, o => o.MapFrom(s => s.Staff.NameSurname));

            #endregion
            #region StaffShift
            CreateMap<AddStaffShiftDto, StaffShift>().ReverseMap();
            CreateMap<ListStaffShiftDto, StaffShift>().ReverseMap();
            CreateMap<StaffShift, ListStaffShiftOnDashboardDto>().ForMember(d => d.NameSurname, o => o.MapFrom(s => s.Staff.NameSurname));
            CreateMap<StaffShift, ListStaffShiftDto>().ForMember(d => d.NameSurname, o => o.MapFrom(s => s.Staff.NameSurname));

            #endregion
            #region StaffOtherInfo
            CreateMap<ListStaffOtherInfoDto, StaffOtherInfo>().ReverseMap();
            #endregion
            #region StaffPayment
            CreateMap<ListStaffPaymentDto, StaffPayment>().ReverseMap();
            CreateMap<AddStaffPaymentDto, StaffPayment>().ReverseMap();
            CreateMap<StaffPayment, ListStaffPaymentDto>().ForMember(d => d.NameSurname, o => o.MapFrom(s => s.Staff.NameSurname));
            CreateMap<StaffPayment, ListStaffPaymentOnDashboardDto>().ForMember(d => d.NameSurname, o => o.MapFrom(s => s.Staff.NameSurname));

            #endregion

            #region Receipt
            CreateMap<ListReceiptDto, Receipt>().ReverseMap();
            #endregion
            #region PublicHoliday
            CreateMap<AddPublicHolidayDto, PublicHoliday>().ReverseMap();
            CreateMap<ListPublicHolidayDto, PublicHoliday>().ReverseMap();
            CreateMap<PublicHoliday, ListStaffPublicDayOnDashboardDto>();
            #endregion
            #region Debit
            CreateMap<ListStaffDebitDto, StaffDebit>().ReverseMap();
            CreateMap<AddStaffDebitDto, StaffDebit>().ReverseMap();
            #endregion
            #region Event
            CreateMap<ListEventDto, Event>().ReverseMap();
            CreateMap<Event, ListStaffEventOnDashboardDto>();
            CreateMap<AddEventDto, Event>().ReverseMap();
            CreateMap<StaffPersonelInfo, ListStaffBirthOnDashboardDto>().ForMember(d => d.NameSurname, o => o.MapFrom(s => s.Staff.NameSurname));
            #endregion
            #region Leave Rule
            CreateMap<ListLeaveRuleDto, LeaveRule>().ReverseMap();
            CreateMap<AddLeaveRuleDto, LeaveRule>().ReverseMap();
            #endregion
            #region Company
            CreateMap<ListCompanyDto, Company>().ReverseMap();
            CreateMap<AddCompanyDto, Company>().ReverseMap();
            #endregion
            #region Role
            CreateMap<ListRoleDto, AppRole>().ReverseMap();
            CreateMap<AddRoleDto, AppRole>().ReverseMap();
            #endregion
            #region Company Branch
            CreateMap<CompanyBranch, ListCompanyBranchDto>().ForMember(d => d.CompanyName, o => o.MapFrom(s => s.Company.CompanyName));
            CreateMap<ListCompanyBranchDto, CompanyBranch>();
            CreateMap<AddCompanyBranchDto, CompanyBranch>().ReverseMap();
            #endregion
            #region AppUser
           CreateMap<ListAppUserDto, AppUser>().ReverseMap();
            #endregion

        }
    }

}
