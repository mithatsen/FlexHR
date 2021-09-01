using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffPaymentDtos;
using FlexHR.DTO.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FlexHR.Web.StringInfo.RoleInfo;

namespace FlexHR.Web.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "ViewPaymentRequestPage,Manager")]
        public IActionResult Index()
        {
            TempData["Active"] = TempdataInfo.Payment;
            var approvedPayment = _mapper.Map<List<ListStaffPaymentWithUserActiveInfoDto>>(_staffPaymentService.Get(p => p.GeneralStatusGeneralSubTypeId == 97 && p.IsActive==true,null,"Staff").ToList());
            var pendingApprovalPayment = _mapper.Map<List<ListStaffPaymentWithUserActiveInfoDto>>(_staffPaymentService.Get(p => p.GeneralStatusGeneralSubTypeId == 96 && p.IsActive == true, null, "Staff").ToList());
            var rejectedPayment = _mapper.Map<List<ListStaffPaymentWithUserActiveInfoDto>>(_staffPaymentService.Get(p => p.GeneralStatusGeneralSubTypeId == 98 && p.IsActive == true, null, "Staff").ToList());

            foreach (var item in approvedPayment)
            {
                item.PaymentType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.PaymentTypeGeneralSubTypeId);
                item.CurrencyType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.CurrencyGeneralSubTypeId);
                item.IsUserActive= _staffService.Get(p => p.StaffId == item.StaffId).FirstOrDefault().IsActive;

            }
            foreach (var item in pendingApprovalPayment)
            {
                item.PaymentType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.PaymentTypeGeneralSubTypeId);
                item.CurrencyType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.CurrencyGeneralSubTypeId);
                item.IsUserActive = _staffService.Get(p => p.StaffId == item.StaffId).FirstOrDefault().IsActive;
            }
            foreach (var item in rejectedPayment)
            {
                item.PaymentType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.PaymentTypeGeneralSubTypeId);
                item.CurrencyType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.CurrencyGeneralSubTypeId);
                item.IsUserActive = _staffService.Get(p => p.StaffId == item.StaffId).FirstOrDefault().IsActive;
            }
            ListPaymentViewModel listPaymentViewModel = new ListPaymentViewModel
            {
                ApprovedPayments = approvedPayment,
                PendingApprovalPayments = pendingApprovalPayment,
                RejectedPayments = rejectedPayment
            };
            return View(listPaymentViewModel);
        }
        [HttpPost]
        public bool ApprovePayment(int id)
        {
            var temp = _staffPaymentService.GetById(id);
            temp.GeneralStatusGeneralSubTypeId = 97;
            try
            {
                _staffPaymentService.Update(temp);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        [HttpPost]
        public bool RejectPayment(int id)
        {
            var temp = _staffPaymentService.GetById(id);
            temp.GeneralStatusGeneralSubTypeId = 98;
            try
            {
                _staffPaymentService.Update(temp);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        } 
        [HttpPost]
        public bool IsPaidPayment(int id)
        {
            var temp = _staffPaymentService.GetById(id);
            temp.IsPaid =true;
            try
            {
                _staffPaymentService.Update(temp);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
