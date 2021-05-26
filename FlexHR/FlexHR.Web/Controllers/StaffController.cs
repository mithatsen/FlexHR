using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffDtos;
using FlexHR.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly IMapper _mapper;
        public StaffController(IStaffService staffService, IMapper mapper)
        {
            _staffService = staffService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var result = _staffService.GetAll();
            return View(_mapper.Map<List<ListStaffDto>>(result));
        }
    }
}
