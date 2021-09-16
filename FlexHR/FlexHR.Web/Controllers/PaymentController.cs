using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffPaymentDtos;
using FlexHR.DTO.ViewModels;
using FlexHR.Entity.Concrete;
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
        private readonly IAppUserService _appUserService;
        private readonly ITakePaymentService _takePaymentService;
        public PaymentController(IGeneralSubTypeService generalSubTypeService, IMapper mapper, IStaffPaymentService staffPaymentService, IStaffService staffService, IAppUserService appUserService, ITakePaymentService takePaymentService)
        {
            _staffService = staffService;
            _generalSubTypeService = generalSubTypeService;
            _mapper = mapper;
            _staffPaymentService = staffPaymentService;
            _appUserService = appUserService;
            _takePaymentService = takePaymentService;
        }
        [Authorize(Roles = "ViewPaymentRequestPage,Manager")]
        public IActionResult Index()
        {
            TempData["Active"] = TempdataInfo.Payment;
            var approvedPayment = _mapper.Map<List<ListStaffPaymentWithUserActiveInfoDto>>(_staffPaymentService.Get(p => p.GeneralStatusGeneralSubTypeId == 97 && p.IsActive == true, null, "Staff").ToList());
            var pendingApprovalPayment = _mapper.Map<List<ListStaffPaymentWithUserActiveInfoDto>>(_staffPaymentService.Get(p => p.GeneralStatusGeneralSubTypeId == 96 && p.IsActive == true, null, "Staff").ToList());
            var rejectedPayment = _mapper.Map<List<ListStaffPaymentWithUserActiveInfoDto>>(_staffPaymentService.Get(p => p.GeneralStatusGeneralSubTypeId == 98 && p.IsActive == true, null, "Staff").ToList());

            foreach (var item in approvedPayment)
            {
                item.PaymentType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.PaymentTypeGeneralSubTypeId);
                item.CurrencyType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.CurrencyGeneralSubTypeId);
                item.IsUserActive = _staffService.Get(p => p.StaffId == item.StaffId).FirstOrDefault().IsActive;

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
            temp.WhoApprovedStaffId = _appUserService.Get(x => x.UserName == User.Identity.Name).FirstOrDefault().StaffId;
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
            temp.IsPaid = true;
            var month = Convert.ToDateTime(temp.PaymentDate).Month;
            var year = Convert.ToDateTime(temp.PaymentDate).Year;
            for (int i = 0; i < Convert.ToInt32(temp.Installment); i++)
            {
                year = month < 12 ? year : year + 1;
                month = month < 12 ? month + 1 : 1;

                _takePaymentService.Add(new TakePayment
                {
                    InstallmentAmount = temp.Amount / (decimal)temp.Installment,
                    StaffPaymentId = temp.StaffPaymentId,
                    PaymentDate = new DateTime(year, month, 1),
                    IsPaid = false,
                    IsActive = true,
                });
            }
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
