using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffSalaryDtos;
using FlexHR.Entity.Concrete;
using FlexHR.Entity.Enums;
using FlexHR.Web.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    [Authorize]
    public class StaffSalaryController : BaseIdentityController
    {
        private readonly IMapper _mapper;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IStaffSalaryService _staffSalaryService;
        private readonly IStaffService _staffService;
        public StaffSalaryController(IMapper mapper, IGeneralSubTypeService generalSubTypeService, UserManager<AppUser> userManager, IStaffService staffService, IStaffSalaryService staffSalaryService) : base(userManager)
        {
            _generalSubTypeService = generalSubTypeService;
            _mapper = mapper;
            _staffService = staffService;
            _staffSalaryService = staffSalaryService;
        }
        [Authorize(Roles = "YeniRol,Manager")]
        public async Task<IActionResult> Index(int id)
        {
            if (await IsAuthority(id))
            {
                var salaryInfo = _staffSalaryService.Get(x => x.StaffId == id).FirstOrDefault();
                ViewBag.CurrencyList = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Currency), "GeneralSubTypeId", "Description");
                ViewBag.PeriodList = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Period), "GeneralSubTypeId", "Description");
                ViewBag.FeeTypeList = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.FeeType), "GeneralSubTypeId", "Description");

                ViewBag.StaffSalaryUpdateStatus = TempData["StaffSalaryUpdateStatus"];
                return View(_mapper.Map<ListStaffSalaryDto>(salaryInfo));
            }
            else
            {
                return RedirectToAction("StatusCode", "Auth", new { code = 404 });
            }

        }
        [Authorize(Roles = "YeniRol,Manager")]
        public IActionResult UpdateStaffSalary(UpdateStaffSalaryDto model)
        {
            try
            {
                var result = _mapper.Map<StaffSalary>(model);

                _staffSalaryService.Update(result);
                TempData["StaffSalaryUpdateStatus"] = "true";
                return RedirectToAction("Index", new { id = model.StaffId });
            }
            catch (Exception)
            {
                return View();
            }
        }
        [Authorize(Roles = "YeniRol,Manager")]
        public async Task<IActionResult> GetStaffSalaryMonthlyList()
        {
            var staffs = _staffService.Get(x => x.IsActive == true);
            var result = _staffSalaryService.GetStaffSalaryMonthly(DateTime.Now);
            List<ListStaffSalaryMonthlyDto> models = new List<ListStaffSalaryMonthlyDto>();
            foreach (var item in staffs)
            {
                var salaryInfo = _staffSalaryService.Get(x => x.StaffId == item.StaffId).FirstOrDefault();
                var incomePerHour = salaryInfo.Salary / salaryInfo.TotalWorkingHour;
                var trackingList = result.Where(x => Convert.ToInt32(x.CardNo) == item.PersonalNo).ToList();
                TimeSpan totalWorkingHour = new TimeSpan(0, 0, 0, 0);
                TimeSpan totalOvertimeHour = new TimeSpan(0, 0, 0, 0);
                var department = "";
                var branch = "";
                if (trackingList.Count != 0)
                {

                    foreach (var personal in trackingList)
                    {
                        department = personal.Department;
                        branch = personal.Branch;
                        var time = (Convert.ToDateTime(personal.CkeckoutTime) - Convert.ToDateTime(personal.EntryTime));

                        if (time.CompareTo(TimeSpan.Zero) < 0)
                        {
                            time = time.Add(TimeSpan.FromHours(24));
                        }
                        if (personal.Overtime != "")
                        {
                            time = time.Subtract(TimeSpan.FromHours(Convert.ToDateTime(personal.Overtime).Hour));
                            time = time.Subtract(TimeSpan.FromMinutes(Convert.ToDateTime(personal.Overtime).Minute));
                            totalOvertimeHour += Convert.ToDateTime(personal.Overtime).TimeOfDay;
                        }
                        totalWorkingHour += time;

                    }
                }
        
                models.Add(new ListStaffSalaryMonthlyDto
                {
                    NameSurname = item.NameSurname,
                    CardNo = item.PersonalNo,
                    Branch=branch,
                    Department=department,
                    IncomePerHour=Math.Round(incomePerHour??0,2),
                    IncomePerShiftHour=salaryInfo.OvertimePayPerHour,
                    TotalWorkingHour = totalWorkingHour.Hours + " sa " + totalWorkingHour.Minutes + " dk ",
                    TotalOvertimeHour = totalOvertimeHour.Hours + " sa " + totalOvertimeHour.Minutes + " dk ",
                    TotalDeservedSalary= Math.Round((incomePerHour * (totalWorkingHour.Hours + totalWorkingHour.Minutes / 60) + salaryInfo.OvertimePayPerHour * (totalOvertimeHour.Hours + totalWorkingHour.Minutes / 60)??0),2),
                    NetSalary=salaryInfo.Salary
                });
            }

            return View(models);
        }
    }


}
