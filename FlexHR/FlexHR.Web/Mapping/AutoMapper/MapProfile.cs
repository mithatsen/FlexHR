﻿using AutoMapper;
using FlexHR.DTO.Dtos.EventDtos;
using FlexHR.DTO.Dtos.ReceiptDtos;
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
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            #region Staff
            CreateMap<ListStaffDto, Staff>().ReverseMap();          
            CreateMap<UpdateStaffDto, Staff>().ReverseMap();
            CreateMap<UpdateStaffDto, StaffPersonelInfo>().ReverseMap();
            CreateMap<UpdateStaffPersonalInfoDto, StaffPersonelInfo>().ReverseMap();
            CreateMap<ListStaffGeneralDto, Staff>().ReverseMap();
            #endregion
            #region StaffCareersta
            CreateMap<ListStaffCareerDto, StaffCareer>().ReverseMap();
            CreateMap<AddStaffCareerDto, StaffCareer>().ReverseMap();
            #endregion

            #region StaffLeave
            CreateMap<AddStaffLeaveDto, StaffLeave>().ReverseMap();
            CreateMap<ListStaffLeaveDto, StaffLeave>().ReverseMap();
            #endregion
            #region StaffShift
            CreateMap<AddStaffShiftDto, StaffShift>().ReverseMap();
            CreateMap<ListStaffShiftDto, StaffShift>().ReverseMap();
            #endregion
            #region StaffOtherInfo
            CreateMap<ListStaffOtherInfoDto, StaffOtherInfo>().ReverseMap();
            #endregion
            #region StaffPayment
            CreateMap<ListStaffPaymentDto, StaffPayment>().ReverseMap();
            CreateMap<AddStaffPaymentDto, StaffPayment>().ReverseMap();
            #endregion
            #region StaffShift
            CreateMap<ListStaffShiftDto, StaffShift>().ReverseMap();
            #endregion
            #region Receipt
            CreateMap<ListReceiptDto, Receipt>().ReverseMap();
            #endregion
            #region PublicHoliday
            CreateMap<AddPublicHolidayDto, PublicHoliday>().ReverseMap();
            #endregion
            #region Debit
            CreateMap<ListStaffDebitDto, StaffDebit>().ReverseMap();
            CreateMap<AddStaffDebitDto, StaffDebit>().ReverseMap();
            #endregion
            #region Event
            CreateMap<ListEventDto, Event>().ReverseMap();
            CreateMap<AddEventDto, Event>().ReverseMap();
           
            #endregion
            
        }
    }
    
}
