using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffOtherInfoDtos;
using FlexHR.Entity.Concrete;
using FlexHR.Entity.Enums;
using FlexHR.Web.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    [Authorize]
    public class StaffOtherInfoController : BaseIdentityController
    {
        private readonly IMapper _mapper;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IStaffOtherInfoService _staffOtherInfoService;
        private readonly ICountryService _countryService;
        private readonly ITownService _townService;
        private readonly ICityService _cityService;
        public StaffOtherInfoController(IMapper mapper, IGeneralSubTypeService generalSubTypeService, IStaffOtherInfoService staffOtherInfoService,
            ICountryService countryService, ITownService townService, ICityService cityService ,UserManager<AppUser> userManager) : base(userManager)
        {
            _generalSubTypeService = generalSubTypeService;
            _mapper = mapper;
            _staffOtherInfoService = staffOtherInfoService;
            _countryService = countryService;
            _townService = townService;
            _cityService = cityService;
        }
        [Authorize(Roles = "ViewStaffOtherInfoPage,Manager,Staff")]
        public async Task<IActionResult> Index(int id)
        {
            if (await IsAuthority(id))
            {
                TownHelper cityIdAndCountryId = new TownHelper();
                var staffOtherInfo = _staffOtherInfoService.GetOtherInfoByStaffId(id);

                if (staffOtherInfo.TownId != null)
                {
                    cityIdAndCountryId = _townService.GetCityIdAndCountryIdByTownId((int)staffOtherInfo.TownId);
                    ViewBag.Cities = new SelectList(_cityService.GetCityListByCountryId(cityIdAndCountryId.CountryId), "CityId", "Name");
                    ViewBag.Towns = new SelectList(_townService.GetTownListByCityId(cityIdAndCountryId.CityId), "TownId", "Name");

                }
                ViewBag.AccountTypeList = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.AccountType), "GeneralSubTypeId", "Description");
                ViewBag.Countries = new SelectList(_countryService.GetAll(), "CountryId", "Name");
                var staffOtherInfoDto = _mapper.Map<ListStaffOtherInfoDto>(staffOtherInfo);
                staffOtherInfoDto.TownHelper = cityIdAndCountryId;

                ViewBag.StaffOtherInfoUpdateStatus = TempData["StaffOtherInfoUpdateStatus"];
                return View(staffOtherInfoDto);
            }
            else
            {
                return RedirectToAction("StatusCode", "Auth", new { code = 404 });
            }
        }
        
        [HttpPost]
        public IActionResult UpdateStaffOtherInfo(ListStaffOtherInfoDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.AccountTypeGeneralSubTypeId = model.AccountTypeGeneralSubTypeId == -1 ? null : model.AccountTypeGeneralSubTypeId;
                    var staffOtherInfo = _mapper.Map<StaffOtherInfo>(model);
                                       
                    _staffOtherInfoService.Update(staffOtherInfo);
                    TempData["StaffOtherInfoUpdateStatus"] = "true";
                    return RedirectToAction("Index", new { id = model.StaffId });
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", new { id = model.StaffId });
                }
           
                
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
    }
}
