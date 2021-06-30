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
            var result = _staffService.Get(x => !x.IsActive, null, "StaffFile");

            ViewBag.ContractTypes = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.ContractType), "GeneralSubTypeId", "Description");
            ViewBag.Roles = new SelectList(_roleService.GetAll(), "RoleId", "Name");

            
            var models = _mapper.Map<List<ListStaffDto>>(result);
            foreach (var item in models)
            {
                var picture = _staffFileService.Get(x => x.StaffId ==item.StaffId  && x.IsActive == true && x.FileGeneralSubTypeId == 3).FirstOrDefault();
            
                item.PictureUrl = picture != null ? picture.FileFullPath + picture.FileName : null;
            }

            return View(models);

        }

        [HttpGet]
        public IActionResult UpdateStaff(int id)
        {
            var result = _staffService.GetAllTables(id);
            var careerResult = _careerService.GetAllTableByStaffId(id);
            var maritialStatusList = _generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.MaritalStatus);
            var degreeOfDisabilityList = _generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.DegreeOfDisability);
            var educationStatusList = _generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.EducationStatus);
            var genderList = _generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Gender);
            var accountTypeList = _generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.AccountType);
            var bloodGroupList = _generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.BloodGroup);
            var educationLevelList = _generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.EducationLevel);
            var temp = _staffGeneralSubTypeService.GetByStaffId(id);
            var temp2 = _staffRoleService.GetUserRoleByStaffId(id);
            var personelInfo = _staffPersonelInfoService.GetPersonelInfoByStaffId(id);
            var staffOtherInfo = _staffOtherInfoService.GetOtherInfoByStaffId(id);
            var staffLeaveList = _staffLeaveService.Get(p => p.StaffId == id && p.IsActive == true);
            var staffShiftList = _staffShiftService.Get(p => p.StaffId == id && p.IsActive == true);
            var staffPaymentList = _staffPaymentService.Get(p => p.StaffId == id);
            var staffFileList = _staffFileService.Get(p => p.StaffId == id);



            ListStaffCareerDto activeCareerDto;

            if (careerResult.Count > 0)
            {
                var activeCareer = careerResult.First();
                activeCareerDto = new ListStaffCareerDto
                {
                    DepartmantName = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(activeCareer.DepartmantGeneralSubTypeId),
                    BranchName = activeCareer.CompanyBranch != null ? activeCareer.CompanyBranch.BranchName : "-",
                    CompanyName = _companyService.GetCompanyNameByCompanyId(activeCareer.CompanyId),
                    Title = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(activeCareer.TitleGeneralSubTypeId),

                };
            }
            else
            {
                activeCareerDto = new ListStaffCareerDto
                {
                    DepartmantName = "-",
                    BranchName = "-",
                    CompanyName = "-",
                    Title = "-"
                };
            }




            TownHelper cityIdAndCountryId = new TownHelper();

            if (staffOtherInfo.TownId != null)
            {
                cityIdAndCountryId = _townService.GetCityIdAndCountryIdByTownId((int)staffOtherInfo.TownId);
                ViewBag.Cities = new SelectList(_cityService.GetCityListByCountryId(cityIdAndCountryId.CountryId), "CityId", "Name");
                ViewBag.Towns = new SelectList(_townService.GetTownListByCityId(cityIdAndCountryId.CityId), "TownId", "Name");
            }

            int contractTypeId = -1;

            ViewBag.Countries = new SelectList(_countryService.GetAll(), "CountryId", "Name");
            ViewBag.Companies = new SelectList(_companyService.GetAll(), "CompanyId", "CompanyName");
            ViewBag.Departments = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Department), "GeneralSubTypeId", "Description");
            ViewBag.ModeOfOperations = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.ModeOfOperation), "GeneralSubTypeId", "Description");
            ViewBag.Titles = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Title), "GeneralSubTypeId", "Description");
            ViewBag.LeaveTypes = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.LeaveType), "GeneralSubTypeId", "Description");
            ViewBag.FileTypes = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.FileType), "GeneralSubTypeId", "Description");



            foreach (var item in temp)
            {
                if (item.GeneralSubType.GeneralTypeId == (int)GeneralTypeEnum.ContractType)
                {
                    contractTypeId = item.GeneralSubTypeId;
                }
            }


            var careerModels = new List<ListStaffCareerDto>();
            foreach (var item in careerResult)
            {
                var careerModel = new ListStaffCareerDto
                {
                    StaffCareerId = item.StaffCareerId,
                    JobStartDate = item.JobStartDate,
                    JobFinishDate = item.JobFinishDate,
                    CompanyName = _companyService.GetCompanyNameByCompanyId(item.CompanyId),
                    BranchName = item.CompanyBranch != null ? item.CompanyBranch.BranchName : "-",
                    IsActive = item.IsActive,
                    ModeOfOperation = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.ModeOfOperationGeneralSubTypeId),
                    DepartmantName = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.DepartmantGeneralSubTypeId),
                    Title = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.TitleGeneralSubTypeId)

                };
                careerModels.Add(careerModel);
            }

            var leaveModels = new List<ListStaffLeaveDto>();
            foreach (var item in staffLeaveList)
            {
                var leaveModel = new ListStaffLeaveDto
                {
                    LeaveStartDate = item.LeaveStartDate,
                    LeaveEndDate = item.LeaveEndDate,
                    LeaveType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.LeaveTypeGeneralSubTypeId),
                    LeaveTypeGeneralSubTypeId = item.LeaveTypeGeneralSubTypeId,
                    Description = item.Description,
                    GeneralStatusGeneralSubTypeId = item.GeneralStatusGeneralSubTypeId,
                    StatusType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.GeneralStatusGeneralSubTypeId),
                    IsMailSentToStaff = item.IsMailSentToStaff,
                    IsSentForApproval = item.IsSentForApproval,
                    StaffLeaveId = item.StaffLeaveId,
                    TotalDay = item.TotalDay
                };
                leaveModels.Add(leaveModel);
            }
            var paymentModels = new List<ListStaffPaymentDto>();
            foreach (var item in staffPaymentList)
            {
                var paymentModel = new ListStaffPaymentDto
                {
                    Amount = item.Amount,
                    CreationDate = item.CreationDate,
                    Description = item.Description,
                    GeneralStatusGeneralSubTypeId = item.GeneralStatusGeneralSubTypeId,
                    IsMailSentToStaff = item.IsMailSentToStaff,
                    IsSentForApproval = item.IsSentForApproval,
                    StaffPaymentId = item.StaffPaymentId,
                    IsPaid = item.IsPaid,
                    PaymentDate = item.PaymentDate,
                    PaymentTypeGeneralSubTypeId = item.PaymentTypeGeneralSubTypeId,
                    PaymentType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.PaymentTypeGeneralSubTypeId)
                };
                paymentModels.Add(paymentModel);
            }

            var shiftModels = new List<ListStaffShiftDto>();
            foreach (var item in staffShiftList)
            {
                var shiftModel = new ListStaffShiftDto
                {
                    StaffShiftId = item.StaffShiftId,
                    Description = item.Description,
                    Duration = item.Duration,
                    IsActive = item.IsActive,
                    StaffId = item.StaffId,
                    StartDate = item.StartDate
                };
                shiftModels.Add(shiftModel);
            }
            var fileModels = new List<ListStaffFileDto>();
            foreach (var item in staffFileList)
            {
                var fileModel = new ListStaffFileDto
                {
                    FileGeneralSubTypeId = item.FileGeneralSubTypeId,
                    FileName = item.FileName,
                    FileFullPath = item.FileFullPath,
                    IsActive = item.IsActive,
                    StaffId = item.StaffId,
                    StaffFileId = item.StaffFileId
                };
                fileModels.Add(fileModel);
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
                ContractTypeList = contractTypeList,
                IsActive = result.IsActive,
                ActiveCareer = activeCareerDto,
                Roles = roleList,
                RoleId = temp2.RoleId,
                JobFinishDate = result.JobFinishDate,
                BloodGroupList = bloodGroupList,
                GenderList = genderList,
                EducationLevelList = educationLevelList,
                MaritialStatusList = maritialStatusList,
                EducationStatusList = educationStatusList,
                DegreeOfDisabilityList = degreeOfDisabilityList,
                ListStaffCareer = careerModels,
                StaffPersonelInfo = personelInfo,
                StaffOtherInfo = staffOtherInfo,
                AccountTypeList = accountTypeList,
                TownHelper = cityIdAndCountryId,
                ContractTypeId = contractTypeId,
                TownId = staffOtherInfo.TownId == null ? 0 : (int)staffOtherInfo.TownId,
                ListStaffLeave = leaveModels,
                ListStaffFile = fileModels,
                ListStaffShift = shiftModels,

                ListStaffPayment = paymentModels

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
                var counter = 0;
                foreach (var item in temp)
                {
                    if (item.GeneralSubType.GeneralTypeId == (int)GeneralTypeEnum.ContractType)
                    {
                        counter++;
                        item.GeneralSubTypeId = model.ContractTypeId;
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
                temp2.RoleId = model.RoleId;
                _staffRoleService.Update(temp2);
                model.Password = "abc";
                model.UserName = "abc";
                _staffService.Update(_mapper.Map<Staff>(model));
                return RedirectToAction("UpdateStaff", "Staff", new { id = model.StaffId }, "tab_1");
            }

            return View();
        }

        [HttpPost]
        public IActionResult UpdateStaffPersonalInfo(UpdateStaffDto model)
        {
            if (ModelState.IsValid)
            {
                model.StaffPersonelInfo.StaffId = model.StaffId;
                _staffPersonelInfoService.Update(model.StaffPersonelInfo);
                return RedirectToAction("UpdateStaff", "Staff", new { id = model.StaffId }, "tab_3");
            }
            return View();
        }
        [HttpPost]
        public IActionResult UpdateStaffOtherInfo(UpdateStaffDto model)
        {
            if (ModelState.IsValid)
            {
                model.StaffOtherInfo.StaffId = model.StaffId;
                _staffOtherInfoService.Update(model.StaffOtherInfo);
                return RedirectToAction("UpdateStaff", "Staff", new { id = model.StaffId }, "tab_4");
            }
            return View();
        }
        public JsonResult TownList(int id)
        {
            var jsonString = JsonConvert.SerializeObject(_townService.GetTownListByCityId(id));
            return Json(jsonString);
        }
        public JsonResult CityList(int id)
        {
            var jsonString = JsonConvert.SerializeObject(_cityService.GetCityListByCountryId(id));
            return Json(jsonString);
        }
        public JsonResult BranchList(int id)
        {
            var jsonString = JsonConvert.SerializeObject(_companyBranchService.GetCompanyBranchListByCompanyId(id));
            return Json(jsonString);
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

        [HttpPost]
        public IActionResult AddStaffCareerWithAjax(AddStaffCareerDto model)
        {
            if (model.CompanyBranchId == -1)
            {
                model.CompanyBranchId = null;
            }
            if (model.JobFinishDate == null || model.JobFinishDate > DateTime.Now)
            {
                model.IsActive = true;
            }
            else
            {
                model.IsActive = false;
            }

            _staffCareerService.Add(_mapper.Map<StaffCareer>(model));


            return Json("tab_2");
        }
        [HttpGet]
        public IActionResult GetUpdateStaffCareerModal(int id)
        {
            var result = _staffCareerService.GetById(id);
            ViewBag.Companies = new SelectList(_companyService.GetAll(), "CompanyId", "CompanyName");
            ViewBag.Departments = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Department), "GeneralSubTypeId", "Description");
            ViewBag.ModeOfOperations = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.ModeOfOperation), "GeneralSubTypeId", "Description");
            ViewBag.Titles = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Title), "GeneralSubTypeId", "Description");
            var careerModel = new ListStaffCareerDto
            {

                JobStartDate = result.JobStartDate,
                JobFinishDate = result.JobFinishDate,
                CompanyBranchId = result.CompanyBranchId,
                TitleGeneralSubTypeId = result.TitleGeneralSubTypeId,
                DepartmantGeneralSubTypeId = result.DepartmantGeneralSubTypeId,
                CompanyId = result.CompanyId,
                ModeOfOperationGeneralSubTypeId = result.ModeOfOperationGeneralSubTypeId,
                StaffCareerId = result.StaffCareerId,
                StaffId = result.StaffId
            };

            return PartialView("GetUpdateStaffCareerModal", careerModel);

        }
        [HttpGet]
        public IActionResult GetUpdateStaffLeaveModal(int id)
        {
            var result = _staffLeaveService.GetById(id);
            ViewBag.LeaveTypes = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.LeaveType), "GeneralSubTypeId", "Description");
            var leaveModel = new ListStaffLeaveDto
            {
                Description = result.Description,
                LeaveStartDate = result.LeaveStartDate,
                LeaveEndDate = result.LeaveEndDate,
                LeaveTypeGeneralSubTypeId = result.LeaveTypeGeneralSubTypeId,
                TotalDay = result.TotalDay,
                StaffId = result.StaffId,
                StaffLeaveId = result.StaffLeaveId,
                GeneralStatusGeneralSubTypeId = result.GeneralStatusGeneralSubTypeId,
                IsActive = result.IsActive

            };

            return PartialView("GetUpdateStaffLeaveModal", leaveModel);

        }
        [HttpGet]
        public IActionResult GetUpdateStaffShiftModal(int id)
        {
            var result = _staffShiftService.GetById(id);

            var shiftModel = new ListStaffShiftDto
            {
                Description = result.Description,
                StartDate = result.StartDate,
                Duration = result.Duration,
                StaffId = result.StaffId,
                StaffShiftId = result.StaffShiftId,
                IsActive = result.IsActive
            };

            return PartialView("GetUpdateStaffShiftModal", shiftModel);

        }

        [HttpPost]
        public IActionResult UpdateStaffLeave(ListStaffLeaveDto model)
        {

            _staffLeaveService.Update(_mapper.Map<StaffLeave>(model));
            return RedirectToAction("UpdateStaff", "Staff", new { id = model.StaffId }, "tab_5");

        }
        [HttpPost]
        public IActionResult UpdateStaffShift(ListStaffShiftDto model)
        {

            _staffShiftService.Update(_mapper.Map<StaffShift>(model));
            return RedirectToAction("UpdateStaff", "Staff", new { id = model.StaffId }, "tab_7");

        }
        [HttpPost]
        public IActionResult UpdateStaffCareer(ListStaffCareerDto model)
        {
            if (model.CompanyBranchId == -1)
            {
                model.CompanyBranchId = null;
            }
            if (model.JobFinishDate == null || model.JobFinishDate > DateTime.Now)
            {
                model.IsActive = true;
            }
            else
            {
                model.IsActive = false;
            }

            _staffCareerService.Update(_mapper.Map<StaffCareer>(model));
            return RedirectToAction("UpdateStaff", "Staff", new { id = model.StaffId }, "tab_2");

        }
        [HttpPost]
        public IActionResult AddStaffLeaveWithAjax(AddStaffLeaveDto model)
        {

            _staffLeaveService.Add(_mapper.Map<StaffLeave>(model));

            return Json(null);
        }
        [HttpPost]
        public IActionResult AddStaffShiftWithAjax(AddStaffShiftDto model)
        {

            _staffShiftService.Add(_mapper.Map<StaffShift>(model));

            return Json(null);
        }

    }
}
