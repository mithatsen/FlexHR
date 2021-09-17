﻿using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffDebitDtos;
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
    public class StaffDebitController : BaseIdentityController
    {
        private readonly IStaffDebitService _staffDebitService;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IMapper _mapper;
        public StaffDebitController(IStaffDebitService staffDebitService, IGeneralSubTypeService generalSubTypeService, IMapper mapper, UserManager<AppUser> userManager) : base(userManager)
        {
            _staffDebitService = staffDebitService;
            _generalSubTypeService = generalSubTypeService;
            _mapper = mapper;
        }
        [Authorize(Roles = "ViewStaffDebitPage,Manager,Staff")]
        public async Task<IActionResult> Index(int id)
        {
            if (await IsAuthority(id))
            {
                ViewBag.StaffId = id;
                var staffDebits = _staffDebitService.Get(p => p.StaffId == id && p.IsActive == true);

                ViewBag.Categories = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Debit), "GeneralSubTypeId", "Description");
                ViewBag.StaffDebitUpdateStatus = TempData["StaffDebitUpdateStatus"];
                var staffDebitDtoList = _mapper.Map<List<ListStaffDebitDto>>(staffDebits);
                foreach (var item in staffDebitDtoList)
                {
                    item.DebitGeneralSubTypeName = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.DebitGeneralSubTypeId);
                }

                return View(staffDebitDtoList);
            }
            else
            {
                return RedirectToAction("StatusCode", "Auth", new { code = 404 });
            }

        }
        [HttpPost]
        public bool AddStaffDebitWithAjax(AddStaffDebitDto model)
        {
            try
            {
                ViewBag.StaffId = model.StaffId;

                _staffDebitService.Add(_mapper.Map<StaffDebit>(model));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
        [HttpGet]
        public IActionResult GetUpdateStaffDebitModal(int id)
        {
            var staffDebit = _staffDebitService.GetById(id);
         
            ViewBag.Categories = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Debit), "GeneralSubTypeId", "Description");

            var x = _mapper.Map<ListStaffDebitDto>(staffDebit);
            return PartialView("_GetUpdateStaffDebitModal", x);

        }
        [HttpPost]
        public IActionResult UpdateStaffDebit(ListStaffDebitDto model)
        {


            _staffDebitService.Update(_mapper.Map<StaffDebit>(model));
            TempData["StaffDebitUpdateStatus"] = "true";

            return RedirectToAction("Index", new { id = model.StaffId });

        }
        [HttpPost]
        public bool DeleteDebit(int id)
        {


            try
            {
                var debit = _staffDebitService.GetById(id);
                debit.IsActive = false;
                _staffDebitService.Update(debit);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
