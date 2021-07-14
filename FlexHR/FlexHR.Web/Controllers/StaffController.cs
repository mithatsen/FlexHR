using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffCareerDtos;
using FlexHR.DTO.Dtos.StaffDtos;
using FlexHR.DTO.Dtos.StaffFileDtos;
using FlexHR.DTO.Dtos.StaffLeaveDtos;
using FlexHR.DTO.Dtos.StaffPaymentDtos;
using FlexHR.DTO.Dtos.StaffPersonalInfoDtos;
using FlexHR.DTO.Dtos.StaffShiftDtos;
using FlexHR.Entity.Concrete;
using FlexHR.Entity.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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
        private readonly IStaffPersonelInfoService _staffPersonelInfoService;
        private readonly IStaffOtherInfoService _staffOtherInfoService;
        private readonly ITownService _townService;
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;
        private readonly ICompanyService _companyService;
        private readonly ICompanyBranchService _companyBranchService;
        private readonly IStaffCareerService _staffCareerService;
        private readonly IStaffLeaveService _staffLeaveService;
        private readonly IStaffShiftService _staffShiftService;
        private readonly IStaffFileService _staffFileService;
        private readonly IStaffPaymentService _staffPaymentService;

        public StaffController(IStaffService staffService, IMapper mapper, IStaffGeneralSubTypeService staffGeneralSubTypeService,
                                     IStaffRoleService staffRoleService, IGeneralSubTypeService generalSubTypeService,
                                     IRoleService roleService, IStaffCareerService careerService, IStaffPersonelInfoService staffPersonelInfoService,
                                     IStaffOtherInfoService staffOtherInfoService, ITownService townService, ICityService cityService, ICountryService countryService,
                                     ICompanyService companyService, ICompanyBranchService companyBranchService, IStaffCareerService staffCareerService,
                                     IStaffLeaveService staffLeaveService, IStaffShiftService staffShiftService,
                                     IStaffPaymentService staffPaymentService, IStaffFileService staffFileService

                                )
        {
            _staffService = staffService;
            _mapper = mapper;
            _staffGeneralSubTypeService = staffGeneralSubTypeService;
            _staffRoleService = staffRoleService;
            _generalSubTypeService = generalSubTypeService;
            _roleService = roleService;
            _careerService = careerService;
            _staffPersonelInfoService = staffPersonelInfoService;
            _staffOtherInfoService = staffOtherInfoService;
            _townService = townService;
            _cityService = cityService;
            _countryService = countryService;
            _companyService = companyService;
            _companyBranchService = companyBranchService;
            _staffCareerService = staffCareerService;
            _staffLeaveService = staffLeaveService;

            _staffShiftService = staffShiftService;
            _staffFileService = staffFileService;
            _staffPaymentService = staffPaymentService;

        }

        public IActionResult Index()
        {
            var result = _staffService.Get(x => x.IsActive, null, "StaffFile");

            ViewBag.ContractTypes = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.ContractType), "GeneralSubTypeId", "Description");
            ViewBag.Roles = new SelectList(_roleService.GetAll(), "RoleId", "Name");


            var models = _mapper.Map<List<ListStaffDto>>(result);
            foreach (var item in models)
            {
                var picture = _staffFileService.Get(x => x.StaffId == item.StaffId && x.IsActive == true && x.FileGeneralSubTypeId == 3).OrderByDescending(x => x.StaffFileId).FirstOrDefault();

                item.PictureUrl = picture != null ? picture.FileFullPath + picture.FileName : null;
            }

            return View(models);

        }




        [HttpPost]
        public IActionResult AddStaffWithAjax(AddStaffDto modal)
        {
            var staffResult = new Staff();
            if (modal.WillUseSystem == false)
            {
                staffResult = _staffService.AddResult(new Staff
                {
                    NameSurname = modal.NameSurname,
                    JobJoinDate = modal.JobJoinDate,
                    IsActive = true,
                    WillUseSystem = modal.WillUseSystem,
                    EmailJob = modal.EmailJob,
                    EmailPersonal = modal.EmailPersonal,
                    JobFinishDate = modal.JobFinishDate,
                    PhoneJob = modal.PhoneJob,
                    PhonePersonal = modal.PhonePersonal,
                });
            }
            else
            {
                staffResult = _staffService.AddResult(new Staff
                {
                    NameSurname = modal.NameSurname,
                    JobJoinDate = modal.JobJoinDate,
                    IsActive = true,
                    UserName = modal.UserName,
                    Password = modal.Password,
                    WillUseSystem = modal.WillUseSystem,
                    EmailJob = modal.EmailJob,
                    EmailPersonal = modal.EmailPersonal,
                    JobFinishDate = modal.JobFinishDate,
                    PhoneJob = modal.PhoneJob,
                    PhonePersonal = modal.PhonePersonal,
                });
            }

            var staffId = staffResult.StaffId;
            if (modal.ContractTypeId != -1)
            {
                _staffGeneralSubTypeService.Add(new StaffGeneralSubType { StaffId = staffId, GeneralSubTypeId = modal.ContractTypeId });
            }
            _staffRoleService.Add(new StaffRole { StaffId = staffId, RoleId = modal.RoleId });
            _staffOtherInfoService.Add(new StaffOtherInfo { StaffId = staffId, IsActive = true });
            _staffPersonelInfoService.Add(new StaffPersonelInfo { StaffId = staffId, IsActive = true });

            return Json("");
        }
    }
}
