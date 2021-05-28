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
        private readonly IStaffGeneralSubTypeService _staffGeneralSubTypeService;
        private readonly IMapper _mapper;
        public StaffController(IStaffService staffService, IMapper mapper, IStaffGeneralSubTypeService staffGeneralSubTypeService)
        {
            _staffService = staffService;
            _mapper = mapper;
            _staffGeneralSubTypeService = staffGeneralSubTypeService;
        }

        public IActionResult Index()
        {
            var result = _staffService.GetAll();
            return View(_mapper.Map<List<ListStaffDto>>(result));
        }
        public IActionResult UpdateStaff(int id)
        {
            var result = _staffService.GetAllTables(id);
            var temp = _staffGeneralSubTypeService.GetByStaffId(id);
            var  departmentName ="";
            foreach (var item in temp)
            {
                if (item.GeneralSubType.GeneralTypeId == 3)
                {
                    departmentName = item.GeneralSubType.Description;
                }

            }


            var model = new UpdateStaffDto
            {
                EmailJob = result.EmailJob,
                EmailPersonal = result.EmailPersonal,
                JobFinishDate = result.JobFinishDate,
                JobJoinDate = result.JobJoinDate,
                NameSurname = result.NameSurname,
                PhoneJob = result.PhoneJob,
                PhonePersonal = result.PhonePersonal,
                StaffId = result.StaffId,
                IsActive = result.IsActive,
                DepartmantName=departmentName
         
            };
            return View(model);
        }
    }
}
