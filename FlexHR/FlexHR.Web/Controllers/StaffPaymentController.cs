using AutoMapper;

using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.ReceiptDtos;
using FlexHR.DTO.Dtos.StaffPaymentDtos;
using FlexHR.Entity.Concrete;
using FlexHR.Entity.Enums;
using FlexHR.Web.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    [Authorize]
    public class StaffPaymentController : BaseIdentityController
    {
        private readonly IStaffService _staffService;
        private readonly IStaffPaymentService _staffPaymentService;
        private readonly IReceiptService _receiptService;
        private readonly IMapper _mapper;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IConfiguration _configuration;
        private readonly IAppUserService _appUserService;
        private readonly ITakePaymentService _takePaymentService;
        public StaffPaymentController(IStaffPaymentService staffPaymentService, IStaffService staffService, IMapper mapper,
            IGeneralSubTypeService generalSubTypeService, IConfiguration configuration, IReceiptService receiptService,
             IAppUserService appUserService, UserManager<AppUser> userManager, ITakePaymentService takePaymentService) : base(userManager)
        {
            _staffPaymentService = staffPaymentService;
            _mapper = mapper;
            _generalSubTypeService = generalSubTypeService;
            _configuration = configuration;
            _receiptService = receiptService;
            _staffService = staffService;
            _appUserService = appUserService;
            _takePaymentService = takePaymentService;
        }
        [Authorize(Roles = "ViewStaffPaymentInfo,Manager,Staff")]
        public async Task<IActionResult> Index(int id)
        {
            if (await IsAuthority(id))
            {
                ViewBag.StaffPaymentUpdateStatus = TempData["StaffPaymentUpdateStatus"];
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
                    paymentModel.Receipts = _receiptService.Get(x => x.StaffPaymentId == item.StaffPaymentId && x.IsActive == true).ToList();
                    paymentModels.Add(paymentModel);
                }

                return View(paymentModels);
            }
            else
            {
                return RedirectToAction("StatusCode", "Auth", new { code = 404 });
            }

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

        public async Task<IActionResult> AddStaffPaymentWithAjax(IFormCollection data, int id)
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
                bool isCheckedApprove = Convert.ToBoolean(data["IsCheckedApprove"]);
                bool isPaidApprove = Convert.ToBoolean(data["IsPaidApprove"]);
                var idddd = Convert.ToInt32(data["staffId"]);
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
                            Amount = decimal.Parse(temp[2].Replace(".", ",")),
                            FileName = item.FileName,
                            FileFullPath = Path.Combine(staffName, "HarcamaFisleri" + "/"),
                            IsActive = true
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
                                Amount = decimal.Parse(temp[2].Replace(".", ",")),

                                FileName = "",
                                FileFullPath = "",
                                IsActive = true
                            };
                            receipts.Add(receipt);
                        }
                    }
                    var x = new StaffPayment
                    {
                        Receipts = receipts,
                        StaffId = Convert.ToInt32(data["staffId"]),
                        Amount = decimal.Parse(amount.Replace(".", ",")),
                        PaymentDate = Convert.ToDateTime(date),
                        CreationDate = DateTime.Now,
                        CurrencyGeneralSubTypeId = Convert.ToInt32(currencyType),
                        Description = LgDescription,
                        GeneralStatusGeneralSubTypeId = isCheckedApprove == true ? 97 : 96,
                        IsActive = true,
                        IsMailSentToStaff = false,
                        IsPaid = isPaidApprove == true ? true : false,
                        IsSentForApproval = false,
                        PaymentTypeGeneralSubTypeId = id,
                        WhoApprovedStaffId = _appUserService.Get(x => x.UserName == User.Identity.Name).FirstOrDefault().StaffId,
                    };
                    _staffPaymentService.Add(x);
                }
                else if (id == 100 || id == 103) // avans ve icra için taksite özel 'take back' tablosuna eklemeler de yapılacak.
                {
                    var m = new StaffPayment
                    {
                        StaffId = Convert.ToInt32(data["staffId"]),
                        Amount = decimal.Parse(amount.Replace(".", ",")),
                        PaymentDate = Convert.ToDateTime(date),
                        CreationDate = DateTime.Now,
                        CurrencyGeneralSubTypeId = Convert.ToInt32(currencyType),
                        Description = SmDescription,
                        GeneralStatusGeneralSubTypeId = isCheckedApprove == true ? 97 : 96,
                        IsActive = true,
                        IsMailSentToStaff = false,
                        IsPaid = isPaidApprove == true ? true : false,
                        IsSentForApproval = false,
                        PaymentTypeGeneralSubTypeId = id,
                        Installment = installment != null ? Convert.ToInt32(installment) : -1,
                        WhoApprovedStaffId = _appUserService.Get(x => x.UserName == User.Identity.Name).FirstOrDefault().StaffId,
                    };
                    var staffPaymentResult = _staffPaymentService.AddResult(m);
                    var month = Convert.ToDateTime(date).Month;
                    var year = Convert.ToDateTime(date).Year;

                    if (isCheckedApprove == true && isPaidApprove==true)
                    {
                        for (int i = 0; i < Convert.ToInt32(installment); i++)
                        {
                            year = month < 12 ? year : year + 1;
                            month = month < 12 ? month + 1 : 1;                        

                            _takePaymentService.Add(new TakePayment
                            {
                                InstallmentAmount = decimal.Parse(amount.Replace(".", ",")) / Convert.ToInt32(installment),
                                StaffPaymentId = staffPaymentResult.StaffPaymentId,
                                PaymentDate = new DateTime(year, month, 1),
                                IsPaid = false,
                                IsActive = true,
                            });
                        }
                    }
               
                }
                else
                {
                    var k = new StaffPayment
                    {
                        StaffId = Convert.ToInt32(data["staffId"]),
                        Amount = decimal.Parse(amount.Replace(".", ",")),
                        PaymentDate = Convert.ToDateTime(date),
                        CreationDate = DateTime.Now,
                        CurrencyGeneralSubTypeId = Convert.ToInt32(currencyType),
                        Description = SmDescription,
                        GeneralStatusGeneralSubTypeId = isCheckedApprove == true ? 97 : 96,
                        IsActive = true,
                        IsMailSentToStaff = false,
                        IsPaid = isPaidApprove == true ? true : false,
                        IsSentForApproval = false,
                        PaymentTypeGeneralSubTypeId = id,
                        FeeTypeGeneralSubTypeId = Convert.ToInt32(feeType),
                        WhoApprovedStaffId = _appUserService.Get(x => x.UserName == User.Identity.Name).FirstOrDefault().StaffId,
                    };
                    _staffPaymentService.Add(k);
                }
                return Json("true");

            }
            return Json("false");

        }
        public async Task<IActionResult> AddStaffPaymentMultipleWithAjax(IFormCollection data, int id)
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
                bool isCheckedApprove = Convert.ToBoolean(data["IsCheckedApprove"]);
                bool isPaidApprove = Convert.ToBoolean(data["IsPaidApprove"]);

                var staffIds = data["staffId"][0].Split(',');

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
                            Amount = decimal.Parse(temp[2].Replace(".", ",")),
                            FileName = item.FileName,
                            FileFullPath = Path.Combine(staffName, "HarcamaFisleri" + "/"),
                            IsActive = true
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
                                Amount = decimal.Parse(temp[2].Replace(".", ",")),

                                FileName = "",
                                FileFullPath = "",
                                IsActive = true
                            };
                            receipts.Add(receipt);
                        }
                    }

                    foreach (var item in staffIds)
                    {
                        var x = new StaffPayment
                        {
                            Receipts = receipts,
                            StaffId = Convert.ToInt32(item),
                            Amount = decimal.Parse(amount.Replace(".", ",")),
                            PaymentDate = Convert.ToDateTime(date),
                            CreationDate = DateTime.Now,
                            CurrencyGeneralSubTypeId = Convert.ToInt32(currencyType),
                            Description = LgDescription,
                            GeneralStatusGeneralSubTypeId = isCheckedApprove == true ? 97 : 96,
                            IsActive = true,
                            IsMailSentToStaff = false,
                            IsPaid = isPaidApprove == true ? true : false,
                            IsSentForApproval = false,
                            PaymentTypeGeneralSubTypeId = id,
                            WhoApprovedStaffId = _appUserService.Get(x => x.UserName == User.Identity.Name).FirstOrDefault().StaffId,
                        };
                        _staffPaymentService.Add(x);
                    }
                }
                else if (id == 100 || id == 103)
                {
                    foreach (var item in staffIds)
                    {
                        var m = new StaffPayment
                        {
                            StaffId = Convert.ToInt32(item),
                            Amount = decimal.Parse(amount.Replace(".", ",")),
                            PaymentDate = Convert.ToDateTime(date),
                            CreationDate = DateTime.Now,
                            CurrencyGeneralSubTypeId = Convert.ToInt32(currencyType),
                            Description = SmDescription,
                            GeneralStatusGeneralSubTypeId = isCheckedApprove == true ? 97 : 96,
                            IsActive = true,
                            IsMailSentToStaff = false,
                            IsPaid = isPaidApprove == true ? true : false,
                            IsSentForApproval = false,
                            PaymentTypeGeneralSubTypeId = id,
                            Installment = installment != null ? Convert.ToInt32(installment) : -1,
                            WhoApprovedStaffId = _appUserService.Get(x => x.UserName == User.Identity.Name).FirstOrDefault().StaffId,
                        };
                       
                        var staffPaymentResult = _staffPaymentService.AddResult(m);
                        var month = Convert.ToDateTime(date).Month;
                        var year = Convert.ToDateTime(date).Year;

                        if (isCheckedApprove == true && isPaidApprove == true)
                        {
                            for (int i = 0; i < Convert.ToInt32(installment); i++)
                            {
                                year = month < 12 ? year : year + 1;
                                month = month < 12 ? month + 1 : 1;
                              
                                _takePaymentService.Add(new TakePayment
                                {
                                    InstallmentAmount = decimal.Parse(amount.Replace(".", ",")) / Convert.ToInt32(installment),
                                    StaffPaymentId = staffPaymentResult.StaffPaymentId,
                                    PaymentDate = new DateTime(year, month, 1),
                                    IsPaid = false,
                                    IsActive = true,
                                });
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item in staffIds)
                    {
                        var k = new StaffPayment
                        {
                            StaffId = Convert.ToInt32(item),
                            Amount = decimal.Parse(amount.Replace(".", ",")),
                            PaymentDate = Convert.ToDateTime(date),
                            CreationDate = DateTime.Now,
                            CurrencyGeneralSubTypeId = Convert.ToInt32(currencyType),
                            Description = SmDescription,
                            GeneralStatusGeneralSubTypeId = isCheckedApprove == true ? 97 : 96,
                            IsActive = true,
                            IsMailSentToStaff = false,
                            IsPaid = isPaidApprove == true ? true : false,
                            IsSentForApproval = false,
                            PaymentTypeGeneralSubTypeId = id,
                            FeeTypeGeneralSubTypeId = Convert.ToInt32(feeType),
                            WhoApprovedStaffId = _appUserService.Get(x => x.UserName == User.Identity.Name).FirstOrDefault().StaffId,
                        };
                        _staffPaymentService.Add(k);
                    }
                }
                return Json("true");
            }
            return Json("false");
        }

        [HttpGet]
        public IActionResult GetAdvancePaymentRequestModal()
        {

            ViewBag.Currencies = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Currency), "GeneralSubTypeId", "Description");
            ViewBag.Staffs = new SelectList(_staffService.GetAll(), "StaffId", "NameSurname");
            int userId = Convert.ToInt32(_userManager.GetUserId(HttpContext.User));
            ViewBag.StaffId = _appUserService.Get(x => x.Id == userId).FirstOrDefault().StaffId;
            return PartialView("_GetAdvancePaymentRequestModal");

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
                temp.Receipts = _receiptService.Get(x => x.StaffPaymentId == id && x.IsActive == true).ToList();
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
        [HttpGet]
        public IActionResult GetPaymentRequestModal()
        {
            int userId = Convert.ToInt32(_userManager.GetUserId(HttpContext.User));
            ViewBag.StaffId = _appUserService.Get(x => x.Id == userId).FirstOrDefault().StaffId;
            ViewBag.Currencies = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Currency), "GeneralSubTypeId", "Description");
            ViewBag.Staffs = new SelectList(_staffService.GetAll(), "StaffId", "NameSurname");
            return PartialView("_GetPaymentRequestModal");
        }
        [HttpPost]
        public IActionResult UpdateStaffPayment(ListStaffPaymentDto model)
        {
            if (ModelState.IsValid)
            {
                _staffPaymentService.Update(_mapper.Map<StaffPayment>(model));
                TempData["StaffPaymentUpdateStatus"] = "true";
                return RedirectToAction("Index", new { id = model.StaffId });
            }
            TempData["StaffPaymentUpdateStatus"] = "false";
            return View();
        }

        public IActionResult GetStaffPaymentWithReceiptInfoModal(int id)
        {


            var receipts = _receiptService.Get(x => x.StaffPaymentId == id && x.IsActive == true).ToList();
            return PartialView("_GetStaffPaymentInfo", _mapper.Map<List<ListReceiptDto>>(receipts));


            //ViewBag.Departments = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Department), "GeneralSubTypeId", "Description");
            //ViewBag.ModeOfOperations = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.ModeOfOperation), "GeneralSubTypeId", "Description");
            //ViewBag.Titles = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Title), "GeneralSubTypeId", "Description");

        }
        [HttpPost]
        public bool DeleteReceipt(int id)
        {
            try
            {
                var model = _receiptService.GetById(id);
                model.IsActive = false;
                _receiptService.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IActionResult> AddReceiptOnUpdatePage(IFormCollection data, int id)
        {

            if (ModelState.IsValid)
            {
                int staffPaymentId = Convert.ToInt32(data["StaffPaymentId"]);
                var result = _staffPaymentService.Get(x => x.StaffPaymentId == staffPaymentId).FirstOrDefault();
                string amount = data["Amount"];
                string date = data["Date"];
                string LgDescription = data["LgDescription"];
                if (result.PaymentTypeGeneralSubTypeId == 99)
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

                        if (temp[0] == "0")
                        {
                            Receipt receipt = new Receipt
                            {
                                Name = temp[1],
                                Vat = Convert.ToDecimal(temp[2]),
                                Amount = decimal.Parse(temp[3].Replace(".", ",")),
                                FileName = item.FileName,
                                FileFullPath = Path.Combine(staffName, "HarcamaFisleri" + "/"),
                                IsActive = true,
                                StaffPaymentId = staffPaymentId
                            };
                            _receiptService.Add(receipt);

                        }
                        else
                        {
                            var tempo = _receiptService.GetById(Convert.ToInt32(temp[0]));
                            tempo.Name = temp[1];
                            tempo.Vat = Convert.ToDecimal(temp[2]);
                            tempo.Amount = decimal.Parse(temp[3].Replace(".", ","));
                            tempo.FileName = item.FileName;
                            tempo.FileFullPath = Path.Combine(staffName, "HarcamaFisleri" + "/");
                            tempo.IsActive = true;
                            tempo.StaffPaymentId = staffPaymentId;
                            _receiptService.Update(tempo);
                        }

                    }
                    foreach (var key in data.Keys)
                    {
                        if (key.Contains("~"))
                        {
                            var temp2 = key.Split("~");
                            if (temp2[0] == "0")
                            {
                                Receipt receipt = new Receipt
                                {
                                    Name = temp2[1],
                                    Vat = Convert.ToDecimal(temp2[2]),
                                    Amount = decimal.Parse(temp2[3].Replace(".", ",")),
                                    FileName = "",
                                    FileFullPath = "",
                                    IsActive = true,
                                    StaffPaymentId = staffPaymentId
                                };
                                _receiptService.Add(receipt);
                            }
                            else
                            {
                                var temp = _receiptService.GetById(Convert.ToInt32(temp2[0]));
                                temp.Name = temp2[1];
                                temp.Vat = Convert.ToDecimal(temp2[2]);
                                temp.Amount = decimal.Parse(temp2[3].Replace(".", ","));
                                temp.FileName = temp.FileName;
                                temp.FileFullPath = temp.FileFullPath;
                                temp.IsActive = true;
                                temp.StaffPaymentId = staffPaymentId;
                                _receiptService.Update(temp);
                            }

                        }
                    }
                    var x = _staffPaymentService.GetById(staffPaymentId);

                    x.Amount = decimal.Parse(amount.Replace(".", ","));
                    x.PaymentDate = Convert.ToDateTime(date);
                    x.Description = LgDescription;
                    x.CurrencyGeneralSubTypeId = Convert.ToInt32(data["CurrencyType"]);

                    _staffPaymentService.Update(x);

                }
                else if (result.PaymentTypeGeneralSubTypeId == 100 || result.PaymentTypeGeneralSubTypeId == 103)
                {
                    var m = new StaffPayment
                    {
                        StaffId = Convert.ToInt32(data["staffId"]),
                        Amount = decimal.Parse(amount.Replace(".", ",")),
                        PaymentDate = Convert.ToDateTime(date),
                        CreationDate = DateTime.Now,
                        GeneralStatusGeneralSubTypeId = 96,
                        IsActive = true,
                        IsMailSentToStaff = false,
                        IsPaid = false,
                        IsSentForApproval = false,
                        PaymentTypeGeneralSubTypeId = id,
                        CurrencyGeneralSubTypeId = Convert.ToInt32(data["CurrencyType"])
                    };
                    _staffPaymentService.Add(m);
                }
                else
                {
                    var k = new StaffPayment
                    {
                        StaffId = Convert.ToInt32(data["staffId"]),
                        Amount = decimal.Parse(amount.Replace(".", ",")),
                        PaymentDate = Convert.ToDateTime(date),
                        CreationDate = DateTime.Now,
                        GeneralStatusGeneralSubTypeId = 96,
                        IsActive = true,
                        IsMailSentToStaff = false,
                        IsPaid = false,
                        IsSentForApproval = false,
                        PaymentTypeGeneralSubTypeId = id,
                        CurrencyGeneralSubTypeId = Convert.ToInt32(data["CurrencyType"])
                    };
                    _staffPaymentService.Add(k);
                }
                return Json("true");

            }

            return Json("false");
        }



    }
}
