﻿using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffPaymentDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class TakePaymentController : Controller
    {
        private readonly IStaffPaymentService _staffPaymentService;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly ITakePaymentService _takePaymentService;
        private readonly IMapper _mapper;
        public TakePaymentController(IStaffPaymentService staffPaymentService, IMapper mapper, IGeneralSubTypeService generalSubTypeService, ITakePaymentService takePaymentService)
        {
            _staffPaymentService = staffPaymentService;
            _mapper = mapper;
            _generalSubTypeService = generalSubTypeService;
            _takePaymentService = takePaymentService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetStaffPayments()
        {
            var paymentList = _staffPaymentService.Get(x => (x.PaymentTypeGeneralSubTypeId == 100 || x.PaymentTypeGeneralSubTypeId == 103) && x.IsPaid == true && x.GeneralStatusGeneralSubTypeId == 97, null, "Staff").ToList();

            var result = _mapper.Map<List<ListPaymentForTakePaymentDto>>(paymentList);
            foreach (var item in result)
            {
                item.PaymentType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.PaymentTypeGeneralSubTypeId);
            }
            return Json(result);

        }
        public IActionResult GetStaffPaymentInfo(int id)
        {
            var paymentList = _takePaymentService.Get(x => x.StaffPaymentId == id && x.IsActive == true).ToList();
            return Json(paymentList);

        }
        public IActionResult ApproveInstallment(int id)
        {
            var paymentList = _takePaymentService.Get(x => x.Id == id).FirstOrDefault();
            paymentList.IsPaid = true;
            _takePaymentService.Update(paymentList);
            return Json(true);

        }
        public IActionResult DeleteInstallment(int id)
        {
            var paymentList = _takePaymentService.Get(x => x.Id == id).FirstOrDefault();
            paymentList.IsActive = false;
            _takePaymentService.Update(paymentList);
            return Json(true);

        }
    }

}