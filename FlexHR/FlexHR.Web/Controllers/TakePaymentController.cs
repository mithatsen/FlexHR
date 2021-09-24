using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffPaymentDtos;
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
        [Authorize(Roles = "ViewTakePaymentPage,Manager")]
        public IActionResult Index()
        {
            TempData["Active"] = TempdataInfo.TakePayment;
            return View();
        }
     
        public IActionResult GetStaffPayments()
        {
            var paymentList = _staffPaymentService.Get(x => (x.PaymentTypeGeneralSubTypeId == 100 || x.PaymentTypeGeneralSubTypeId == 103) && x.IsPaid == true && x.GeneralStatusGeneralSubTypeId == 97, null, "Staff").ToList();
            decimal remainingAmount=0;
            var result = _mapper.Map<List<ListPaymentForTakePaymentDto>>(paymentList);
            foreach (var item in result)
            {
                var remainingPayments= _takePaymentService.Get(x => x.StaffPaymentId == item.StaffPaymentId && x.IsPaid==false && x.IsActive==true).ToList();
                foreach (var temp in remainingPayments)
                {
                     remainingAmount += temp.InstallmentAmount;
                }
                item.PaymentType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.PaymentTypeGeneralSubTypeId);
                item.CurrencyType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.CurrencyGeneralSubTypeId);
                item.RemainingAmount = remainingAmount;
            }
            return Json(result);

        }
  
        public IActionResult GetStaffPaymentInfo(int id)
        {
            var paymentList = _takePaymentService.Get(x => x.StaffPaymentId == id && x.IsActive == true,null,"StaffPayment").OrderBy(x => x.PaymentDate).ToList();
            var result = _mapper.Map<List<ListTakePaymentDto>>(paymentList);
            foreach (var item in result)
            {
                item.CurrencyType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.CurrencyGeneralSubTypeId);
            }
            return Json(result);

        }
        public IActionResult ApproveInstallment(int id)
        {
            var paymentList = _takePaymentService.Get(x => x.Id == id).FirstOrDefault();
            paymentList.IsPaid = true;
            _takePaymentService.Update(paymentList);
            return Json(true);

        }
        [HttpPost]
        public IActionResult AddInstallment(AddInstallmentDto model)
        {
            _takePaymentService.Add(_mapper.Map<TakePayment>(model));

            return Json(true);

        }
        [HttpPost]
        public IActionResult UpdateInstallment(ListInstallmentDto model)
        {
            model.IsActive = true;
            _takePaymentService.Update(_mapper.Map<TakePayment>(model));
            return Json(true);

        }
        public IActionResult DeleteInstallment(int id)
        {
            var paymentList = _takePaymentService.Get(x => x.Id == id).FirstOrDefault();
            paymentList.IsActive = false;
            _takePaymentService.Update(paymentList);
            return Json(true);
        }
        [HttpGet]
        public IActionResult GetInstallmentUpdateModal(int id)
        {
            var result = _takePaymentService.Get(x => x.Id == id).FirstOrDefault();
            var avd = _mapper.Map<ListInstallmentDto>(result);
            return PartialView("_GetInstallmentUpdateModal", avd);
        }
    }

}
