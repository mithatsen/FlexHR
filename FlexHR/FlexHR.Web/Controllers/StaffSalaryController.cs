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
        public StaffSalaryController(IMapper mapper, IGeneralSubTypeService generalSubTypeService, UserManager<AppUser> userManager, IStaffSalaryService staffSalaryService) : base(userManager)
        {
            _generalSubTypeService = generalSubTypeService;
            _mapper = mapper;
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

    }
}
