using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffPaymentDtos;
using FlexHR.Entity.Concrete;
using FlexHR.Entity.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class StaffPaymentController : Controller
    {
        private readonly IStaffPaymentService _staffPaymentService;
        private readonly IMapper _mapper;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        public StaffPaymentController(IStaffPaymentService staffPaymentService, IMapper mapper, IGeneralSubTypeService generalSubTypeService)
        {
            _staffPaymentService = staffPaymentService;
            _mapper = mapper;
            _generalSubTypeService = generalSubTypeService;
        }
        public IActionResult Index(int id)
        {
          
            ViewBag.StaffId = id;
            ViewBag.PaymentTypeList = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.PaymentType), "GeneralSubTypeId", "Description");
            ViewBag.Currencies = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Currency), "GeneralSubTypeId", "Description");
            ViewBag.FeeTypes = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.FeeType), "GeneralSubTypeId", "Description");
            var staffPaymentList = _staffPaymentService.Get(p => p.StaffId == id && p.IsActive==true);
            var paymentModels = new List<ListStaffPaymentDto>();
            foreach (var item in staffPaymentList)
            {
                var paymentModel = _mapper.Map<ListStaffPaymentDto>(item);
                paymentModel.CurrencyType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.CurrencyGeneralSubTypeId);
         
                paymentModel.PaymentType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.PaymentTypeGeneralSubTypeId);
                paymentModels.Add(paymentModel);
            }
            return View(paymentModels);
        }
        [HttpPost]
        public bool DeleteStaffPayment(int id)
        {
            try
            {
                var model = _staffPaymentService.GetById(id);
                model.IsActive = false;
                _staffPaymentService.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IActionResult AddStaffPaymentWithAjax(AddStaffPaymentDto model ,int id, Receipt receipts)   // buraya dto oluşturulacak gelen veriler maplenerek veritanına atılacak 
        {
            if (ModelState.IsValid)
            {
                if (id == 99)
                {

                }
                else if (id == 100 || id == 103)
                {
                    model.PaymentTypeGeneralSubTypeId = id;
                    _staffPaymentService.Add(_mapper.Map<StaffPayment>(model));
                }
                else
                {
                    model.PaymentTypeGeneralSubTypeId = id;
                    _staffPaymentService.Add(_mapper.Map<StaffPayment>(model));
                }
                return Json("true");

            }

            return Json("false");

        }


    }
}
