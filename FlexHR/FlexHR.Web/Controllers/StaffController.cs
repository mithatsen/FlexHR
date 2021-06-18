using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffCareerDtos;
using FlexHR.DTO.Dtos.StaffDtos;
using FlexHR.Entity.Concrete;
using FlexHR.Entity.Enums;
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
        private readonly IStaffRoleService _staffRoleService;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IRoleService _roleService;
        private readonly IStaffCareerService _careerService;
        public StaffController  (    IStaffService staffService, IMapper mapper, IStaffGeneralSubTypeService staffGeneralSubTypeService,
                                     IStaffRoleService staffRoleService, IGeneralSubTypeService generalSubTypeService,
                                     IRoleService roleService, IStaffCareerService careerService
                                )
        {
            _staffService = staffService;
            _mapper = mapper;
            _staffGeneralSubTypeService = staffGeneralSubTypeService;
            _staffRoleService = staffRoleService;
            _generalSubTypeService = generalSubTypeService;
            _roleService = roleService;
            _careerService = careerService;
        }

        public IActionResult Index()
        {
            var result = _staffService.GetAll();
            return View(_mapper.Map<List<ListStaffDto>>(result));
        }

        [HttpGet]
        public IActionResult UpdateStaff(int id)
        {
            var result = _staffService.GetAllTables(id);
            var careerResult = _careerService.GetAllTableByStaffId(id);
            var temp = _staffGeneralSubTypeService.GetByStaffId(id);
            var temp2 = _staffRoleService.GetUserRoleByStaffId(id);
            var departmentName = "";
            var superscription = "";
            var contractType = "";
            var modeOfOperation = "";

            var staffInfo = _staffGeneralSubTypeService.GetGeneralSubTypeByStaffGeneralSubTypeList(temp);
            for (int i = 0; i < staffInfo.Count; i++)
            {
                int generalTypeId = Convert.ToInt32(staffInfo.GetKey(i));
                if (Convert.ToInt32(staffInfo.GetKey(i)) == (int)GeneralTypeEnum.Department)
                {
                    departmentName = staffInfo[i];
                }
                else if (Convert.ToInt32(staffInfo.GetKey(i)) == (int)GeneralTypeEnum.Title)
                {
                    superscription = staffInfo[i];
                }
                else if (generalTypeId == (int)GeneralTypeEnum.ContractType)
                {
                    contractType = staffInfo[i];
                }
                else if(Convert.ToInt32(staffInfo.GetKey(i)) == (int)GeneralTypeEnum.ModeOfOperation)
                {
                    modeOfOperation = staffInfo[i];
                }

            }
            var careerModels= new List<ListStaffCareerDto>();
            foreach (var item in careerResult)
            {
                var careerModel = new ListStaffCareerDto
                {
                  
                    JobStartDate = item.JobStartDate,
                    JobFinishDate = item.JobFinishDate,                   
                    CompanyName = item.CompanyBranch.Company.CompanyName,
                    BranchName = item.CompanyBranch.BranchName,
                    IsActive = item.IsActive,               
                    ModeOfOperation = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.ModeOfOperationGeneralSubTypeId),                  
                    DepartmantName = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.DepartmantGeneralSubTypeId),
                    Title = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.TitleGeneralSubTypeId)

                };
                careerModels.Add(careerModel);
            }
            var roleList = _roleService.GetAll();
            var contractTypeList = _generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.ContractType);
            var model = new UpdateStaffDto
            {
                StaffId = result.StaffId,
                EmailJob = result.EmailJob,
                EmailPersonal = result.EmailPersonal,
                JobJoinDate = result.JobJoinDate,
                NameSurname = result.NameSurname,
                PhoneJob = result.PhoneJob,
                PhonePersonal = result.PhonePersonal,                             
                DepartmantName = departmentName,
                Superscription = superscription,
                ContractTypeList = contractTypeList,
                IsActive = result.IsActive,
                ContractType = contractType,
                Roles = roleList,
                RoleId = temp2.RoleId,
                JobFinishDate = result.JobFinishDate,
                
                ListStaffCareer=careerModels
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateStaffGeneral(UpdateStaffDto model)
        {
            if (ModelState.IsValid)
            {
                var temp = _staffGeneralSubTypeService.GetByStaffId(model.StaffId);
                var temp2 = _staffRoleService.GetUserRoleByStaffId(model.StaffId);

                foreach (var item in temp)
                {
                    if (item.GeneralSubType.GeneralTypeId== (int)GeneralTypeEnum.ContractType)
                    {
                        item.GeneralSubTypeId = model.ContractTypeId;
                    }
                }
                temp2.RoleId = model.RoleId;
                _staffRoleService.Update(temp2);
                model.Password = "abc";
                model.UserName = "abc";
                _staffService.Update(_mapper.Map<Staff>(model));
                return RedirectToAction("UpdateStaff", new { id = model.StaffId });
            }

            return View();
        }
    }
}
