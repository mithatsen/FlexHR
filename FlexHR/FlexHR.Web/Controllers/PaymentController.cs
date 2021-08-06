using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffPaymentDtos;
using FlexHR.DTO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IStaffPaymentService _staffPaymentService;
        private readonly IStaffService _staffService;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IMapper _mapper;
        public PaymentController(IGeneralSubTypeService generalSubTypeService, IMapper mapper, IStaffPaymentService staffPaymentService, IStaffService staffService)
        {
            _staffService = staffService;
            _generalSubTypeService = generalSubTypeService;
            _mapper = mapper;
            _staffPaymentService = staffPaymentService;
        }
        public IActionResult Index()

        {
            var approvedPayment = _mapper.Map<List<ListStaffPaymentDto>>(_staffPaymentService.Get(p => p.GeneralStatusGeneralSubTypeId == 97).ToList());
            var pendingApprovalPayment = _mapper.Map<List<ListStaffPaymentDto>>(_staffPaymentService.Get(p => p.GeneralStatusGeneralSubTypeId == 96).ToList());
            var rejectedPayment = _mapper.Map<List<ListStaffPaymentDto>>(_staffPaymentService.Get(p => p.GeneralStatusGeneralSubTypeId == 98).ToList());

            foreach (var item in approvedPayment)
            {
                item.PaymentType = _generalSubTypeService.GetById(item.PaymentTypeGeneralSubTypeId).Description;
            }
            foreach (var item in pendingApprovalPayment)
            {
                item.PaymentType = _generalSubTypeService.GetById(item.PaymentTypeGeneralSubTypeId).Description;
            }
            foreach (var item in rejectedPayment)
            {
                item.PaymentType = _generalSubTypeService.GetById(item.PaymentTypeGeneralSubTypeId).Description;
            }
            ListPaymentViewModel listPaymentViewModel = new ListPaymentViewModel
            {
                ApprovedPayments = approvedPayment,
                PendingApprovalPayments = pendingApprovalPayment,
                RejectedPayments = rejectedPayment
            };
            return View(listPaymentViewModel);
        }

    }
}
