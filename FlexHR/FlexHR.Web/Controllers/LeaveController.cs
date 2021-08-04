using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.LeaveDtos;
using FlexHR.DTO.Dtos.StaffLeaveDtos;
using FlexHR.DTO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{

    public class LeaveController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly ILeaveRuleService _leaveRuleService;
        private readonly IStaffLeaveService _staffLeaveService;
        private readonly IStaffCareerService _staffCareerService;
        private readonly IStaffPersonelInfoService _personelInfoService;
        private readonly IStaffService _staffService;
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IMapper _mapper;
        public LeaveController(IStaffLeaveService staffLeaveService, IGeneralSubTypeService generalSubTypeService, IMapper mapper, ILeaveTypeService leaveTypeService,
            IStaffCareerService staffCareerService, IStaffService staffService, IStaffPersonelInfoService personelInfoService, ICompanyService companyService,
             ILeaveRuleService leaveRuleService)
        {
            _companyService = companyService;
            _personelInfoService = personelInfoService;
            _staffService = staffService;
            _staffCareerService = staffCareerService;
            _staffLeaveService = staffLeaveService;
            _generalSubTypeService = generalSubTypeService;
            _mapper = mapper;
            _leaveTypeService = leaveTypeService;
            _leaveRuleService = leaveRuleService;
        }
        public IActionResult Index()

        {
            var approvedLeaves = _mapper.Map<List<ListStaffLeaveDto>>(_staffLeaveService.Get(p => p.GeneralStatusGeneralSubTypeId == 97).ToList());
            var pendingApprovalLeaves = _mapper.Map<List<ListStaffLeaveDto>>(_staffLeaveService.Get(p => p.GeneralStatusGeneralSubTypeId == 96).ToList());
            var rejectedLeaves = _mapper.Map<List<ListStaffLeaveDto>>(_staffLeaveService.Get(p => p.GeneralStatusGeneralSubTypeId == 98).ToList());
            var upcomingLeaves = _mapper.Map<List<ListStaffLeaveDto>>(_staffLeaveService.Get(p => p.GeneralStatusGeneralSubTypeId == 97 && p.LeaveStartDate > DateTime.Now).ToList());

            foreach (var item in approvedLeaves)
            {
                item.LeaveType = _leaveTypeService.GetById(item.LeaveTypeId).Name;
            }
            foreach (var item in pendingApprovalLeaves)
            {
                item.LeaveType = _leaveTypeService.GetById(item.LeaveTypeId).Name;
            }
            foreach (var item in rejectedLeaves)
            {
                item.LeaveType = _leaveTypeService.GetById(item.LeaveTypeId).Name;
            }
            foreach (var item in upcomingLeaves)
            {
                item.LeaveType = _leaveTypeService.GetById(item.LeaveTypeId).Name;
            }

            ListLeaveViewModel listLeaveViewModel = new ListLeaveViewModel
            {
                ApprovedLeaves = approvedLeaves,
                PendingApprovalLeaves = pendingApprovalLeaves,
                RejectedLeaves = rejectedLeaves,
                UpcomingLeaves = upcomingLeaves
            };
            return View(listLeaveViewModel);
        }

        public IActionResult StaffLeaveRemainInfo()
        {
            List<ListLeaveInfoAllStaffDto> models = new List<ListLeaveInfoAllStaffDto>();
            var staffs = _staffService.GetAll();

            foreach (var item in staffs)
            {
                var totalDayDeserved = CalculateTotalLeaveAmountDeservedBySeniority(item.JobJoinDate);
                var totalDayUsed = _staffLeaveService.Get(p => p.StaffId == item.StaffId && p.IsActive == true && p.GeneralStatusGeneralSubTypeId == 97 && p.LeaveTypeId == 14).Sum(p => p.TotalDay);
                var staffCareer = _staffCareerService.Get(x => x.IsActive == true && x.StaffId == item.StaffId, null, "CompanyBranch").OrderByDescending(p => p.JobStartDate).FirstOrDefault();
                var staffPersonel = _personelInfoService.Get(x => x.IsActive == true && x.StaffId == item.StaffId, null).FirstOrDefault();
                ListLeaveInfoAllStaffDto model = new ListLeaveInfoAllStaffDto()
                {
                    StaffId = item.StaffId,
                    NameSurname = item.NameSurname,
                    IdNumber = staffPersonel.IdNumber,
                    CompanyName = _companyService.GetCompanyNameByCompanyId(staffCareer.CompanyId),
                    CompanyBranchName = staffCareer.CompanyBranch != null ? staffCareer.CompanyBranch.BranchName : "-",
                    DepartmantName = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(staffCareer.DepartmantGeneralSubTypeId),
                    TitleName = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(staffCareer.TitleGeneralSubTypeId),
                    JobStartDate = item.JobJoinDate,
                    TotalDayDeserved = totalDayDeserved,
                    TotalDayUsed = totalDayUsed,
                    TotalDayRemain = totalDayDeserved - totalDayUsed,
                    IsActive = item.IsActive
                };
                models.Add(model);
            }
            return View(models);
        }
        public IActionResult StaffLeaveMonthlyInfo(DateTime dateTime)
        {
            List<ListLeaveInfoMonthlyDto> models = new List<ListLeaveInfoMonthlyDto>();
            var staffs = _staffService.GetAll();
            DateTime date = dateTime.Year != 0001 ? dateTime : DateTime.Now;
            foreach (var item in staffs)
            {
                var totalDayUsed = _staffLeaveService.Get(p => p.StaffId == item.StaffId && p.IsActive == true && p.GeneralStatusGeneralSubTypeId == 97 && p.LeaveTypeId == 14).Sum(p => p.TotalDay);
                var staffPersonel = _personelInfoService.Get(x => x.IsActive == true && x.StaffId == item.StaffId, null).FirstOrDefault();
                var staffLeaves = _staffLeaveService.Get(x => x.IsActive == true && x.StaffId==item.StaffId && x.LeaveStartDate.Year == date.Year && x.LeaveStartDate.Month==date.Month,null,"LeaveType" );
                ViewBag.Filter = date.ToString("Y");
                foreach (var item2 in staffLeaves)
                {
                    ListLeaveInfoMonthlyDto model = new ListLeaveInfoMonthlyDto()
                    {
                        StaffId = item.StaffId,
                        NameSurname = item.NameSurname,
                        IdNumber = staffPersonel.IdNumber,
                        Description = item2.Description,
                        FinishDate=item2.LeaveEndDate,
                        StartDate=item2.LeaveStartDate,
                        LeaveType=item2.LeaveType.Name,
                        GeneralStatusId=item2.GeneralStatusGeneralSubTypeId,
                        TotalDay=item2.TotalDay
                    };
                    models.Add(model);
                }
                
            }
            return View(models);
        }
        [HttpPost]
        public IActionResult StaffLeaveMonthlyInfos(DateTime dateTime)
        {
            return RedirectToAction("StaffLeaveMonthlyInfo", new { dateTime = dateTime });
        }


        public int CalculateTotalLeaveAmountDeservedBySeniority(DateTime startDate)
        {
            var models = _leaveRuleService.GetAll().OrderBy(p => p.SeniorityYear);
            var seniorityLevel = (DateTime.Now - startDate).Days / 365;
            int oldCount = 0;
            int leaveAmount = 14;
            var totalDayDeserved = 0;
            foreach (var item in models)
            {
                if (seniorityLevel > item.SeniorityYear)
                {
                    totalDayDeserved += (item.SeniorityYear - oldCount) * leaveAmount;
                }
                else
                {
                    totalDayDeserved += (seniorityLevel - oldCount) * leaveAmount;
                    leaveAmount += item.AditionalLeaveAmount;
                    oldCount = item.SeniorityYear;
                    break;
                }
                leaveAmount += item.AditionalLeaveAmount;
                oldCount = item.SeniorityYear;

            }
            if (seniorityLevel - oldCount > 0)
            {
                totalDayDeserved += (seniorityLevel - oldCount) * leaveAmount;
            }


            return totalDayDeserved;
        }

    }
}
