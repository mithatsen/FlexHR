using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.ViewComponents
{
    public class StaffSubHeader : ViewComponent
    {
        private readonly IStaffService _staffService;
        private readonly IMapper _mapper;
        public StaffSubHeader(IStaffService staffService, IMapper mapper)
        {
            _staffService = staffService;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke(int id)
        {
            return View(_mapper.Map<ListStaffDto>(_staffService.GetById(id)));
        }
    }
}
