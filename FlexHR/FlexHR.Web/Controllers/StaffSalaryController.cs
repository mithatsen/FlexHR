using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffPaymentDtos;
using FlexHR.DTO.Dtos.StaffSalaryDtos;
using FlexHR.DTO.ViewModels;
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
using static FlexHR.Web.StringInfo.RoleInfo;

namespace FlexHR.Web.Controllers
{
    [Authorize]
    public class StaffSalaryController : BaseIdentityController
    {
        private readonly IMapper _mapper;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IStaffSalaryService _staffSalaryService;
        private readonly IStaffService _staffService;
        private readonly ICompanyBranchService _companyBranchService;
        private readonly IStaffCareerService _staffCareerService;
        private readonly ITakePaymentService _takePaymentService;
        private readonly IStaffPersonelInfoService _staffPersonelInfoService;
        private readonly IJobRotationHistoryService _jobRotationHistoryService;
        private readonly IStaffLeaveService _staffLeaveService;
        private readonly IStaffShiftService _staffShiftService;
        private readonly IPublicHolidayService _publicHolidayService;
        private readonly IStaffPaymentService _staffPaymentService;
        private readonly ICompanyService _companyService;

        public StaffSalaryController(IMapper mapper, ITakePaymentService takePaymentService, IGeneralSubTypeService generalSubTypeService, UserManager<AppUser> userManager, IStaffService staffService,
            IStaffSalaryService staffSalaryService, ICompanyBranchService companyBranchService, IStaffCareerService staffCareerService, IJobRotationHistoryService jobRotationHistoryService,
            IStaffPersonelInfoService staffPersonelInfoService, IStaffLeaveService staffLeaveService, IStaffShiftService staffShiftService, IPublicHolidayService publicHolidayService,
            IStaffPaymentService staffPaymentService, ICompanyService companyService) : base(userManager)
        {
            _generalSubTypeService = generalSubTypeService;
            _mapper = mapper;
            _staffService = staffService;
            _staffSalaryService = staffSalaryService;
            _companyBranchService = companyBranchService;
            _staffCareerService = staffCareerService;
            _takePaymentService = takePaymentService;
            _jobRotationHistoryService = jobRotationHistoryService;
            _staffPersonelInfoService = staffPersonelInfoService;
            _staffLeaveService = staffLeaveService;
            _staffShiftService = staffShiftService;
            _publicHolidayService = publicHolidayService;
            _staffPaymentService = staffPaymentService;
            _companyService = companyService;
        }
        [Authorize(Roles = "ViewStaffSalaryPage,Manager,Staff")]
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
        [Authorize(Roles = "ViewSalaryMonthlyListPage,Manager")]
        public IActionResult GetStaffSalaryMonthlyList(DateTime dateTime)
        {
            TempData["Active"] = TempdataInfo.Salary;
            DateTime date = dateTime.Year != 0001 ? dateTime : DateTime.Now;
            var staffs = _staffService.Get(x => x.IsActive == true, null, "StaffCareer");
            var result = _staffSalaryService.GetStaffSalaryMonthly(date);

            List<ListStaffSalaryMonthlyDto> models = new List<ListStaffSalaryMonthlyDto>();
            foreach (var item in staffs)
            {

                var careerResult = _staffCareerService.Get(x => x.IsActive == true && x.StaffId == item.StaffId, null, "CompanyBranch").OrderByDescending(p => p.JobStartDate).FirstOrDefault();
                var department = careerResult != null ? _generalSubTypeService.GetDescriptionByGeneralSubTypeId(careerResult.DepartmantGeneralSubTypeId).ToString() : "";
                var branch = careerResult != null ? careerResult.CompanyBranch.BranchName : "";

                models.Add(new ListStaffSalaryMonthlyDto
                {
                    StaffId = item.StaffId,
                    NameSurname = item.NameSurname,
                    CardNo = item.PersonalNo,
                    Branch = branch,
                    Department = department
                });
            }
            StaffSalaryMonthlyViewModal listModel = new StaffSalaryMonthlyViewModal { filterDate = date, ListStaffSalaryMonthly = models };
            return View(listModel);
        }
        [HttpGet]
        public IActionResult GetDebtMonthlyModal(int id, int monthDate, int yearDate)
        {
            var takePayments = _takePaymentService.Get(x => x.PaymentDate.Month == monthDate && x.PaymentDate.Year == yearDate, null, "StaffPayment").Where(x => x.StaffPayment.StaffId == id).ToList();
            var takePaymentVmList = new List<GetMonthlyTakePaymentViewModel>();
            foreach (var temp in takePayments)
            {
                var takePaymentVm = new GetMonthlyTakePaymentViewModel
                {
                    CurrencyType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(temp.StaffPayment.CurrencyGeneralSubTypeId),
                    InstallmentAmount = temp.InstallmentAmount,
                    PaymentType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(temp.StaffPayment.PaymentTypeGeneralSubTypeId)
                };
                takePaymentVmList.Add(takePaymentVm);
            }
            return PartialView("_GetStaffDebtMonthlyModal", takePaymentVmList);
        }
        [HttpGet]
        public IActionResult GetSalaryMonthlyModal(int id, int monthDate, int yearDate)
        {
            var staff = _staffService.GetById(id)
;
            var cardNo = staff.PersonalNo;
            var careerResult = _staffCareerService.Get(x => x.IsActive == true && x.StaffId == staff.StaffId, null, "CompanyBranch").OrderByDescending(p => p.JobStartDate).FirstOrDefault();


            var salary = _staffSalaryService.Get(x => x.StaffId == id).FirstOrDefault();
            if (salary!=null)
            {
                if (salary.AgiPayment != null && salary.Salary != null && salary.PayPerHour != null && salary.PrivatePension != null)
                {
                    var daySalary = salary.Salary / 30;
                    int days = DateTime.DaysInMonth(yearDate, monthDate);
                    List<DateTime> dates = new List<DateTime>();
                    for (int i = 1; i <= days; i++)
                    {
                        dates.Add(new DateTime(yearDate, monthDate, i));
                    }
                    //bir ayda kaç pazar var
                    int weekHolidayDays = dates.Where(d => d.DayOfWeek == DayOfWeek.Sunday).Count();
                    var weekBreak = new StaffSalaryMonthlyHelper { Day = weekHolidayDays, Amounts = Decimal.Round((weekHolidayDays * daySalary), 2) };

                    var totalSalaryWithAgi = salary != null ? (decimal)salary.Salary + (decimal)salary.AgiPayment : 0;
                    var shifts = _staffShiftService.Get(x => x.GeneralStatusGeneralSubTypeId == 97 && x.IsActive == true && x.StaffId == id && x.StartDate.Month == monthDate && x.StartDate.Year == yearDate);
                    var leaves = _staffLeaveService.Get(x => x.GeneralStatusGeneralSubTypeId == 97 && x.IsActive == true && x.StaffId == id && x.LeaveStartDate.Month == monthDate && x.LeaveStartDate.Year == yearDate, null, "LeaveType").ToList();
                    var advancePayment = _staffPaymentService.Get(x => x.GeneralStatusGeneralSubTypeId == 97 && x.IsPaid == true && x.IsActive == true && x.PaymentTypeGeneralSubTypeId == 101 && x.StaffId == id && x.PaymentDate.Month == monthDate && x.PaymentDate.Year == yearDate);
                    var takePaymentsAdvanceAmount = _takePaymentService.Get(x => x.PaymentDate.Month == monthDate && x.PaymentDate.Year == yearDate && x.IsActive == true, null, "StaffPayment").Where(x => x.StaffPayment.StaffId == id && x.StaffPayment.PaymentTypeGeneralSubTypeId == 100).Sum(x => x.InstallmentAmount);
                    var takePaymentsExecutiveAmount = _takePaymentService.Get(x => x.PaymentDate.Month == monthDate && x.PaymentDate.Year == yearDate && x.IsActive == true, null, "StaffPayment").Where(x => x.StaffPayment.StaffId == id && x.StaffPayment.PaymentTypeGeneralSubTypeId == 103).Sum(x => x.InstallmentAmount);
                    var publicHolidays = _publicHolidayService.Get(x => x.IsActive == true && x.Start.Month == monthDate && x.Start.Year == yearDate).ToList();


                    /////////////////////////////////mesailer
                    var SundayShiftTotalMinute = shifts.Where(x => x.StartDate.DayOfWeek == DayOfWeek.Sunday).Sum(x => x.ShiftHour) * 60 + shifts.Where(x => x.StartDate.DayOfWeek == DayOfWeek.Sunday).Sum(x => x.ShiftMinute);
                    var NormalShiftTotalMinute = shifts.Where(x => x.StartDate.DayOfWeek != DayOfWeek.Sunday).Sum(x => x.ShiftHour) * 60 + shifts.Where(x => x.StartDate.DayOfWeek != DayOfWeek.Sunday).Sum(x => x.ShiftMinute);

                    foreach (var item in publicHolidays)
                    {
                        if (item.Start.DayOfWeek != DayOfWeek.Sunday)
                        {
                            var temp = shifts.Where(x => x.StartDate.ToShortDateString() == item.Start.ToShortDateString()).Sum(x => x.ShiftHour) * 60 + shifts.Where(x => x.StartDate == item.Start).Sum(x => x.ShiftMinute);
                            SundayShiftTotalMinute += temp;
                            NormalShiftTotalMinute -= temp;
                        }
                    }


                    var sundayShift = new StaffSalaryMonthlyHelper { Hour = ((float)SundayShiftTotalMinute / 60), Amounts = Decimal.Round((decimal)salary.PayPerHour * Convert.ToDecimal((float)SundayShiftTotalMinute / 60) * 2, 2) };
                    var normalShift = new StaffSalaryMonthlyHelper { Hour = ((float)NormalShiftTotalMinute / 60), Amounts = Decimal.Round((decimal)salary.PayPerHour * Convert.ToDecimal((float)NormalShiftTotalMinute / 60) * 3 / 2, 2) };


                    ////////////////////////////////// report
                    var totalReportDay = _staffService.GetStaffReportDayMonthly(new DateTime(yearDate, monthDate, 1), cardNo);
                    var reportDeduction = daySalary * totalReportDay;
                    var report = new StaffSalaryMonthlyHelper { Day = totalReportDay, Amounts = Decimal.Round(reportDeduction, 2) };

                    ////////////////////////////////// ücretsiz izin, ücretli izin
                    var totalFreeLeaveDay = leaves.Where(x => x.LeaveType.IsFree == true).Sum(x => x.TotalDay);
                    var freeLeaveDeduction = daySalary * totalFreeLeaveDay;
                    var totalLeaveDay = leaves.Where(x => x.LeaveType.IsFree == false).Sum(x => x.TotalDay);
                    var leaveAddition = daySalary * totalLeaveDay;

                    var holidayWithoutPay = new StaffSalaryMonthlyHelper { Day = totalFreeLeaveDay, Amounts = Decimal.Round(freeLeaveDeduction, 2) };
                    var holidayWithPay = new StaffSalaryMonthlyHelper { Day = totalLeaveDay, Amounts = Decimal.Round(leaveAddition, 2) };

                    //////////////////////////////////devamsızlık
                    var absenceTotal = _staffService.GetAbsenceInformationMonthly(new DateTime(yearDate, monthDate, 1), id);
                    var totalAmount = (decimal)(daySalary * absenceTotal.Day + salary.PayPerHour * Convert.ToDecimal(absenceTotal.Hour));
                    absenceTotal.Amounts = Decimal.Round(totalAmount, 2);

                    ////////////////////////////////genel çalışma saati
                    var totalGeneralWorkDayCount = 30 - (weekHolidayDays + totalLeaveDay);
                    var normalWorking = new StaffSalaryMonthlyHelper { Day = totalGeneralWorkDayCount, Amounts = Decimal.Round((totalGeneralWorkDayCount * daySalary), 2) };
                    var idNumber = _staffPersonelInfoService.Get(x => x.StaffId == id).FirstOrDefault().IdNumber;

                    var company = careerResult != null ? _companyService.Get(x => x.CompanyId == careerResult.CompanyId).FirstOrDefault() : null;

                    ListStaffSalaryMonthylyDetailInfo model = new ListStaffSalaryMonthylyDetailInfo
                    {
                        CompanyLogo = company != null ? company.CompanyLogo : "",
                        AdditionIncome1 = (decimal)_staffPaymentService.Get(x => x.GeneralStatusGeneralSubTypeId == 97 && x.IsPaid == true && x.IsActive == true && x.PaymentTypeGeneralSubTypeId == 129 && x.StaffId == id && x.PaymentDate.Month == monthDate && x.PaymentDate.Year == yearDate).Sum(x => x.Amount),
                        Deduction1 = (decimal)_staffPaymentService.Get(x => x.GeneralStatusGeneralSubTypeId == 97 && x.IsPaid == true && x.IsActive == true && x.PaymentTypeGeneralSubTypeId == 130 && x.StaffId == id && x.PaymentDate.Month == monthDate && x.PaymentDate.Year == yearDate).Sum(x => x.Amount),
                        IdNumber = idNumber,
                        NameSurname = staff.NameSurname,
                        JobJoinDate = staff.JobJoinDate,
                        PayrollDate = new DateTime(yearDate, monthDate, 1),
                        Branch = careerResult != null ? (careerResult.CompanyBranch != null ? careerResult.CompanyBranch.BranchName : "-") : "-",
                        Title = careerResult != null ? _generalSubTypeService.GetDescriptionByGeneralSubTypeId(careerResult.TitleGeneralSubTypeId) : "-",
                        PaymentPerHour = salary != null ? (decimal)salary.PayPerHour : 0,
                        SalaryWithoutAgi = salary != null ? salary.Salary : 0,
                        Agi = salary != null ? (decimal)salary.AgiPayment : 0,
                        SalaryWithAgi = totalSalaryWithAgi,
                        HolidayWithoutPay = holidayWithoutPay,
                        Shift = normalShift,
                        SundayShift = sundayShift,
                        Bonus = advancePayment.Sum(x => x.Amount),
                        AdvanceDeduction = Convert.ToDecimal(takePaymentsAdvanceAmount),
                        ExecutiveDeduction = Convert.ToDecimal(takePaymentsExecutiveAmount),
                        PrivatePensionDeduction = salary.PrivatePension != null ? (decimal)salary.PrivatePension : 0,
                        Report = report,
                        Absence = absenceTotal,
                        NormalWorking = normalWorking,
                        WeekBreak = weekBreak,
                        HolidaysWithPay = holidayWithPay,
                        Perks = _staffPaymentService.Get(x => x.PaymentTypeGeneralSubTypeId == 128 && x.CreationDate.Month == monthDate && x.CreationDate.Year == yearDate && x.IsActive == true).ToList().Sum(x => x.Amount)
                    };

                    return PartialView("_GetStaffSalaryMonthlyModal", model);
                }
                else
                {
                    return PartialView("_GetStaffSalaryMonthlyModal");
                }
            }           
            else
            {
                return PartialView("_GetStaffSalaryMonthlyModal");
            }


        }
    }



}

