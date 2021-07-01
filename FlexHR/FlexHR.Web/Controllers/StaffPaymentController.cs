using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffPaymentDtos;
using Microsoft.AspNetCore.Mvc;
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
            var staffPaymentList = _staffPaymentService.Get(p => p.StaffId == id && p.IsActive==true);
            var paymentModels = new List<ListStaffPaymentDto>();
            foreach (var item in staffPaymentList)
            {
                var paymentModel = _mapper.Map<ListStaffPaymentDto>(item);
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
    }
}
