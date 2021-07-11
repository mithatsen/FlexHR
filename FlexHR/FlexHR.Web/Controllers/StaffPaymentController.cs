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
        private readonly IReceiptService _receiptService;
        private readonly IMapper _mapper;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IConfiguration _configuration;
        public StaffPaymentController(IStaffPaymentService staffPaymentService, IMapper mapper, IGeneralSubTypeService generalSubTypeService, IConfiguration configuration, IReceiptService receiptService)
        {
            _staffPaymentService = staffPaymentService;
            _mapper = mapper;
            _generalSubTypeService = generalSubTypeService;
            _configuration = configuration;
            _receiptService = receiptService;
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
                paymentModel.Receipts = _receiptService.Get(x => x.StaffPaymentId == item.StaffPaymentId).ToList();
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

        public async Task<IActionResult> AddStaffPaymentWithAjax(IFormCollection data, int id)   // buraya dto oluşturulacak gelen veriler maplenerek veritanına atılacak 
        {


            if (ModelState.IsValid)
            {
                string amount = data["Amount"];
                string date = data["Date"];
                string SmDescription = data["SmDescription"];
                string LgDescription = data["LgDescription"];
                string currencyType = data["CurrencyType"];
                string feeType = data["FeeType"];
                string installment = data["Installment"];
                if (id == 99)
                {

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
                                FileFullPath = Path.Combine(staffName, "HarcamaFisleri" + "/"),

                            };
                            receipts.Add(receipt);

                        }
                        foreach (var item in data.Keys)
                        {
                        if (item.Contains("~"))
                        {
                            var temp = item.Split("~");
                            Receipt receipt = new Receipt
                            {
                                Name = temp[0],
                                Vat = Convert.ToDecimal(temp[1]),
                                Amount = Convert.ToDecimal(temp[2]),
                                FileName = "",
                                FileFullPath = "",

                            };
                            receipts.Add(receipt);
                        }
                        }

                    //else
                    //{
                    //    for (int i = 0; i < data.Keys.Count; i++)
                    //    {
                    //        //if (data.Keys[i].Contains("~"))
                    //        //{

                    //        //}
                    //    }
                    //    //Receipt receipt = new Receipt
                    //    //{
                    //    //    Name = temp[0],
                    //    //    Vat = Convert.ToDecimal(temp[1]),
                    //    //    Amount = Convert.ToDecimal(temp[2]),
                    //    //    FileName = item.FileName,
                    //    //    FileFullPath = Path.Combine(staffName, "HarcamaFisleri" + "/"),

                    //    //};
                    //    //receipts.Add(receipt);
                    //}

                    var x = new StaffPayment
                    {
                        Receipts = receipts,
                        StaffId = Convert.ToInt32(data["staffId"]),
                        Amount = Convert.ToDecimal(amount),
                        PaymentDate = Convert.ToDateTime(date),
                        CreationDate = DateTime.Now,
                        CurrencyGeneralSubTypeId = Convert.ToInt32(currencyType),
                        Description = LgDescription,
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
                    var m = new StaffPayment
                    {
                        StaffId = Convert.ToInt32(data["staffId"]),
                        Amount = Convert.ToDecimal(amount),
                        PaymentDate = Convert.ToDateTime(date),
                        CreationDate = DateTime.Now,
                        CurrencyGeneralSubTypeId = Convert.ToInt32(currencyType),
                        Description = SmDescription,
                        GeneralStatusGeneralSubTypeId = 96,
                        IsActive = true,
                        IsMailSentToStaff = false,
                        IsPaid = false,
                        IsSentForApproval = false,
                        PaymentTypeGeneralSubTypeId = id,
                        Installment = Convert.ToInt32(installment),
                    };
                    _staffPaymentService.Add(m);
                }
                else
                {
                    var k = new StaffPayment
                    {
                        StaffId = Convert.ToInt32(data["staffId"]),
                        Amount = Convert.ToDecimal(amount),
                        PaymentDate = Convert.ToDateTime(date),
                        CreationDate = DateTime.Now,
                        CurrencyGeneralSubTypeId = Convert.ToInt32(currencyType),
                        Description = SmDescription,
                        GeneralStatusGeneralSubTypeId = 96,
                        IsActive = true,
                        IsMailSentToStaff = false,
                        IsPaid = false,
                        IsSentForApproval = false,
                        PaymentTypeGeneralSubTypeId = id,
                        FeeTypeGeneralSubTypeId = Convert.ToInt32(feeType)
                    };
                    _staffPaymentService.Add(k);
                }
                return Json("true");

            }

            return Json("false");

        }
        [HttpGet]
        public IActionResult GetUpdateStaffPaymentModal(int id)
        {
            var staffPayment = _staffPaymentService.GetById(id);
            // var staffPaymentGetReceipts = _staffPaymentService.Get(x => x.StaffPaymentId == id, null, "Receipt").ToList();
            ViewBag.Currencies = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Currency), "GeneralSubTypeId", "Description");
            ViewBag.FeeTypes = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.FeeType), "GeneralSubTypeId", "Description");
            var paymentTypeId = staffPayment.PaymentTypeGeneralSubTypeId;
            if (paymentTypeId == 99)
            {
                var temp = _mapper.Map<ListStaffPaymentDto>(staffPayment);
                temp.Receipts = _receiptService.Get(x => x.StaffPaymentId == id).ToList();
                return PartialView("_GetUpdateStaffPaymentModal1", temp);
            }
            else if (paymentTypeId == 100 || paymentTypeId == 103)
            {
                var temp = _mapper.Map<ListStaffPaymentDto>(staffPayment);
                return PartialView("_GetUpdateStaffPaymentModal2", temp);
            }
            else
            {

                var temp = _mapper.Map<ListStaffPaymentDto>(staffPayment);
                return PartialView("_GetUpdateStaffPaymentModal3", temp);
            }
            //ViewBag.Departments = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Department), "GeneralSubTypeId", "Description");
            //ViewBag.ModeOfOperations = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.ModeOfOperation), "GeneralSubTypeId", "Description");
            //ViewBag.Titles = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Title), "GeneralSubTypeId", "Description");

        }
        [HttpPost]
        public IActionResult UpdateStaffPayment(ListStaffPaymentDto model)
        {
            if (ModelState.IsValid)
            {
                _staffPaymentService.Update(_mapper.Map<StaffPayment>(model));
                return RedirectToAction("Index", new { id = model.StaffId });
            }
            return View();
        }

        public IActionResult GetStaffPaymentWithReceiptInfoModal(int id)
        {
         
        
            var receipts = _receiptService.Get(x => x.StaffPaymentId == id).ToList();
            return PartialView("_GetStaffPaymentInfo", _mapper.Map<List<ListReceiptDto>>(receipts));
        
              
            //ViewBag.Departments = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Department), "GeneralSubTypeId", "Description");
            //ViewBag.ModeOfOperations = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.ModeOfOperation), "GeneralSubTypeId", "Description");
            //ViewBag.Titles = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Title), "GeneralSubTypeId", "Description");

        }
    }
}
