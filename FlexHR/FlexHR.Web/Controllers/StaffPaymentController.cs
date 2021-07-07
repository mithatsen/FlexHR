using AutoMapper;

using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.ReceiptDtos;
using FlexHR.DTO.Dtos.StaffPaymentDtos;
using FlexHR.Entity.Concrete;
using FlexHR.Entity.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class StaffPaymentController : Controller
    {
        private readonly IStaffPaymentService _staffPaymentService;
        private readonly IMapper _mapper;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IConfiguration _configuration;
        public StaffPaymentController(IStaffPaymentService staffPaymentService, IMapper mapper, IGeneralSubTypeService generalSubTypeService, IConfiguration configuration)
        {
            _staffPaymentService = staffPaymentService;
            _mapper = mapper;
            _generalSubTypeService = generalSubTypeService;
            _configuration = configuration;
        }
        public IActionResult Index(int id)
        {

            ViewBag.StaffId = id;
            ViewBag.PaymentTypeList = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.PaymentType), "GeneralSubTypeId", "Description");
            ViewBag.Currencies = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Currency), "GeneralSubTypeId", "Description");
            ViewBag.FeeTypes = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.FeeType), "GeneralSubTypeId", "Description");
            var staffPaymentList = _staffPaymentService.Get(p => p.StaffId == id && p.IsActive == true);
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

        public async Task<IActionResult> AddStaffPaymentWithAjax(IFormCollection data,int id)   // buraya dto oluşturulacak gelen veriler maplenerek veritanına atılacak 
        {

            if (ModelState.IsValid)
            {
                if (id == 99)
                {
                    string amount = data["Amount"];
                    string date = data["Date"];
                    string description = data["Description"];
                    string currencyType = data["CurrencyType"];
                    List<Receipt> receipts = new List<Receipt>(); //datadan gelen fişleri listeye ekledik.
                    foreach (var item in data.Files)
                    {

                        var temp = item.Name.Split("~");

                        var staffName = "Staff_" + data["staffId"];

                        var fullPath = _configuration.GetValue<string>("FullPath:DefaultPath");
                        var folderPath = Path.Combine(fullPath, staffName);
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        var filePath = Path.Combine(folderPath, "HarcamaFisleri" + "/");
                        if (!Directory.Exists(filePath))
                            Directory.CreateDirectory(filePath);
                        var imagePath = Path.Combine(filePath + item.FileName);
                        if (item.Length > 0)
                        {
                            //dosyamızı kaydediyoruz.

                            using (var stream = new FileStream(imagePath, FileMode.Create))
                            {
                                await item.CopyToAsync(stream);
                            }
                        }

                        Receipt receipt = new Receipt
                        {
                            Name = temp[0],
                            Vat = Convert.ToDecimal(temp[1]),
                            Amount = Convert.ToDecimal(temp[2]),
                            FileName = item.FileName,
                            FileFullPath = Path.Combine(staffName,"HarcamaFisleri" + "/"),

                        };
                        receipts.Add(receipt);

                    }
                    var x = new StaffPayment
                    {
                        Receipts = receipts,
                        StaffId = Convert.ToInt32(data["staffId"]),
                        Amount = Convert.ToDecimal(amount),
                        PaymentDate = Convert.ToDateTime(date),
                        CreationDate = DateTime.Now,
                        CurrencyGeneralSubTypeId = Convert.ToInt32(currencyType),
                        Description = description,
                        GeneralStatusGeneralSubTypeId = 96,
                        IsActive = true,
                        IsMailSentToStaff = false,
                        IsPaid = false,
                        IsSentForApproval = false,
                        PaymentTypeGeneralSubTypeId = id
                    };
                    _staffPaymentService.Add(x);

                }
                else if (id == 100 || id == 103)
                {
                    
                }
                else
                {
                }
                return Json("true");

            }

            return Json("false");

        }


    }
}
