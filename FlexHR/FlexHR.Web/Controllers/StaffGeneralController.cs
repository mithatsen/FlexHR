using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffGeneralDtos;
using FlexHR.Entity.Concrete;
using FlexHR.Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class StaffGeneralController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly IStaffGeneralSubTypeService _staffGeneralSubTypeService;
        private readonly IStaffRoleService _staffRoleService;
        private readonly IRoleService _roleService;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IMapper _mapper;
        public StaffGeneralController(IStaffService staffService, IStaffGeneralSubTypeService staffGeneralSubTypeService, IStaffRoleService staffRoleService, IGeneralSubTypeService generalSubTypeService, IRoleService roleService, IMapper mapper)
        {
            _staffService = staffService;
            _staffGeneralSubTypeService = staffGeneralSubTypeService;
            _staffRoleService = staffRoleService;
            _generalSubTypeService = generalSubTypeService;
            _roleService = roleService;
            _mapper = mapper;
        }
        public IActionResult Index(int id)
        {
            var staff = _staffService.GetAllTables(id);
            var staffGeneralSubTypes = _staffGeneralSubTypeService.GetByStaffId(id);
            var staffRole = _staffRoleService.GetUserRoleByStaffId(id);
            int contractTypeId = -1;
            foreach (var item in staffGeneralSubTypes)
            {
                if (item.GeneralSubType.GeneralTypeId == (int)GeneralTypeEnum.ContractType)
                {
                    contractTypeId = item.GeneralSubTypeId;
                }
            }

            ViewBag.Roles = new SelectList(_roleService.GetAll(), "RoleId", "Name");
            ViewBag.ContractTypes = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.ContractType), "GeneralSubTypeId", "Description");
    
            ListStaffGeneralDto listStaffGeneralDto = new ListStaffGeneralDto
            {
                ContractTypeId = contractTypeId,
                RoleId = staffRole.RoleId,
                EmailJob = staff.EmailJob,
                StaffId = staff.StaffId,
                EmailPersonal = staff.EmailPersonal,
                JobJoinDate = staff.JobJoinDate,
                NameSurname = staff.NameSurname,
                PhoneJob = staff.PhoneJob,
                PhonePersonal = staff.PhonePersonal,
                JobFinishDate = staff.JobFinishDate

            };
            return View(listStaffGeneralDto);
        }
        [HttpPost]
        public IActionResult UpdateStaffGeneral(ListStaffGeneralDto model)
        {
            if (ModelState.IsValid)
            {
                var staffGeneralSubTypes = _staffGeneralSubTypeService.GetByStaffId(model.StaffId);
                var staffRole = _staffRoleService.GetUserRoleByStaffId(model.StaffId);
                var counter = 0;
                foreach (var item in staffGeneralSubTypes)
                {
                    if (item.GeneralSubType.GeneralTypeId == (int)GeneralTypeEnum.ContractType)
                    {
                        counter++;
                        item.GeneralSubTypeId = model.ContractTypeId;
                        item.GeneralSubType = null;
                        _staffGeneralSubTypeService.Update(item);
                    }
                }
                if (counter == 0 && model.ContractTypeId != 0)
                {
                    _staffGeneralSubTypeService.Add(new StaffGeneralSubType
                    {
                        GeneralSubTypeId = model.ContractTypeId,
                        StaffId = model.StaffId
                    });
                }
                staffRole.RoleId = model.RoleId;
                _staffRoleService.Update(staffRole);
                model.Password = "abc";
                model.UserName = "abc";


                _staffService.Update(_mapper.Map<Staff>(model));
                return RedirectToAction("Index", new {id=model.StaffId });
            }

            return View();
        }
    }
}
