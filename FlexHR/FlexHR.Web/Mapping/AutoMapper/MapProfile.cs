﻿using AutoMapper;
using FlexHR.DTO.Dtos.StaffDtos;
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
            #endregion
        }
    }
}