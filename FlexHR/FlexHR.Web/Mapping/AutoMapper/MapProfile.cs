using AutoMapper;
using FlexHR.DTO.Dtos.StaffCareerDtos;
using FlexHR.DTO.Dtos.StaffDtos;
using FlexHR.DTO.Dtos.StaffLeaveDtos;
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
            CreateMap<UpdateStaffPersonalInfoDto, Staff>().ReverseMap();
            #endregion
            #region StaffCareer
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
        }
    }
}
