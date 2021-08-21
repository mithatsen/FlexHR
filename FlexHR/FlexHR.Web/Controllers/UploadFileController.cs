using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.ViewModels;
using FlexHR.Entity.Concrete;
using FlexHR.Entity.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FlexHR.Web.StringInfo.RoleInfo;

namespace FlexHR.Web.Controllers
{
    public class UploadFileController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly ICompanyFileService _companyFileService;
        private readonly IFileColumnService _fileColumnService;
        public UploadFileController(IFileColumnService fileColumnService, IConfiguration configuration, IMapper mapper, ICompanyFileService companyFileService, IGeneralSubTypeService generalSubTypeService)
        {
            _configuration = configuration;
            _generalSubTypeService = generalSubTypeService;
            _mapper = mapper;
            _companyFileService = companyFileService;
            _fileColumnService = fileColumnService;
        }
        public IActionResult Index()
        {
            TempData["Active"] = TempdataInfo.FileProcess;
            ViewBag.FileGeneralUpdateStatus = TempData["FileGeneralUpdateStatus"];
            return View();
        }
        public IActionResult Refectory()
        {
            TempData["Active"] = TempdataInfo.Refectory;
            ViewBag.FileGeneralUpdateStatus = TempData["FileGeneralUpdateStatus"];
            return View();
        }

        public static string ClearTurkishCharacter(string _dirtyText)
        {
            var text = _dirtyText;
            var unaccentedText = String.Join("", text.Normalize(NormalizationForm.FormD).Where(c => char.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark));
            return unaccentedText.Replace("ı", "i");
        }
        [HttpPost]
        public async Task<IActionResult> AddExcelWithAjax(AddExcelViewModel model)
        {
            if (model.file != null)
            {
                string fileName = "";
                if (model.CategoryId == EnumFileType.RefectoryFile.GetHashCode())
                {
                    fileName = "Refectory";
                }
                else if (model.CategoryId == EnumFileType.StaffTrackingFile.GetHashCode())
                {
                    fileName = "StaffTracking";
                }
                //dosyanın uzantısını aldık
                string extension = Path.GetExtension(model.file.FileName);
                string categoryNameFolder = "Excel";
                string categoryTypeFolder = categoryNameFolder + "/" + fileName;
                var fullPath = _configuration.GetValue<string>("FullPath:DefaultPath");
                var path = _fileColumnService.FileUploadCreateFolder(new FileUploadViewModel
                {
                    folderName = categoryTypeFolder,
                    UploadDate = model.Date,
                    xlsFileName = model.file.FileName,
                    xlsPath = fullPath
                });
                var result = _companyFileService.Get(x => x.FileFullPath == path + "/" + model.file.FileName).FirstOrDefault();
                if (result != null)
                {
                    //update
                    if (model.file.Length > 0)
                    {
                        //dosyamızı kaydediyoruz.
                        using (var stream = new FileStream(result.FileFullPath, FileMode.Create))
                        {
                            await model.file.CopyToAsync(stream);
                        }
                    }
                    //eski dataya nasıl ulaşcaz servisi yok generic tablonun
                    var fileUploadResult = _fileColumnService.LoadDataFromExcel(new FileUploadViewModel
                    {
                        file = model.file,
                        xlsFileName = model.file.FileName,
                        folderName = categoryNameFolder,
                        xlsPath = result.FileFullPath,
                        fileUploadTypeID = model.CategoryId,
                        tableName = fileName,
                        UploadDate = model.Date
                    });
                    if (!fileUploadResult.IsSuccess)
                    {
                        //databsaeden de sil exceli
                        System.IO.File.Delete(path + "/" + model.file.FileName);
                    }
                    GenericResultViewModel genericResultView = new GenericResultViewModel()
                    {
                        IsSuccess = fileUploadResult.IsSuccess,
                        Message = fileUploadResult.Message
                    };
                    TempData["FileGeneralUpdateStatus"] = JsonConvert.SerializeObject(genericResultView);
                }
                else
                {
                    //add
                    _companyFileService.Add(new CompanyFile()
                    {
                        FileFullPath = path + "/" + model.file.FileName,
                        FileName = model.file.FileName,
                        FileGeneralSubTypeId = model.CategoryId,
                        IsActive = true,
                    });
                    if (model.file.Length > 0)
                    {
                        //dosyamızı kaydediyoruz.
                        using (var stream = new FileStream(path + "/" + model.file.FileName, FileMode.Create))
                        {
                            await model.file.CopyToAsync(stream);
                        }
                    }
                    var fileUploadResult = _fileColumnService.LoadDataFromExcel(new FileUploadViewModel
                    {
                        file = model.file,
                        xlsFileName = model.file.FileName,
                        folderName = categoryNameFolder,
                        xlsPath = path + "/" + model.file.FileName,
                        fileUploadTypeID = model.CategoryId,
                        tableName = fileName,
                        UploadDate = model.Date
                    });
                    if (!fileUploadResult.IsSuccess)
                    {
                        //databsaeden de sil exceli
                        System.IO.File.Delete(path + "/" + model.file.FileName);
                    }
                    GenericResultViewModel genericResultView = new GenericResultViewModel()
                    {
                        IsSuccess = fileUploadResult.IsSuccess,
                        Message = fileUploadResult.Message
                    };
                    TempData["FileGeneralUpdateStatus"] = JsonConvert.SerializeObject(genericResultView);
                    if (fileName== "StaffTracking")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Refectory");
                    }
                    
                }



                // var fileNameUpdate = model.Date.ToString("d") + "_" + fileName + extension;



                //foreach (var item in result)
                //{
                //    //hatalı bak buraya
                //    if (fileName == item.FileName)
                //    {
                //        var folderPathUpdate = Path.Combine(fullPath);
                //        if (!Directory.Exists(folderPathUpdate))
                //            Directory.CreateDirectory(folderPathUpdate);
                //        //Excel klasörü oluşturuldu
                //        var filePathUpdate = Path.Combine(folderPathUpdate, categoryNameFolder + "/");
                //        if (!Directory.Exists(filePathUpdate))
                //            Directory.CreateDirectory(filePathUpdate);
                //        //
                //        filePathUpdate = Path.Combine(filePathUpdate, categoryTypeFolder + "/");
                //        if (!Directory.Exists(filePathUpdate))
                //            Directory.CreateDirectory(filePathUpdate);
                //        var excelPathUpdate = Path.Combine(filePathUpdate + model.Date.ToString("d") + "_" + fileName + extension);
                //        if (model.file.Length > 0)
                //        {
                //            //dosyamızı kaydediyoruz.
                //            using (var stream = new FileStream(excelPathUpdate, FileMode.Create))
                //            {
                //                await model.file.CopyToAsync(stream);
                //            }
                //        }
                //        TempData["FileGeneralUpdateStatus"] = "true";
                //        if (model.CategoryId == 127)
                //        {
                //            return RedirectToAction("Refectory");
                //        }
                //        else if (model.CategoryId == 129)
                //        {
                //            return RedirectToAction("Index");
                //        }

                //    }
                //}
                ////Flex > Files klasörü oluşturuldu
                //var folderPath = Path.Combine(fullPath);
                //if (!Directory.Exists(folderPath))
                //    Directory.CreateDirectory(folderPath);
                ////Excel klasörü oluşturuldu
                //var filePath = Path.Combine(folderPath, categoryNameFolder + "/");
                //if (!Directory.Exists(filePath))
                //    Directory.CreateDirectory(filePath);
                ////
                //filePath = Path.Combine(filePath, categoryTypeFolder + "/");
                //if (!Directory.Exists(filePath))
                //    Directory.CreateDirectory(filePath);
                //var excelPath = Path.Combine(filePath + model.Date.ToString("d") + "_" + fileName + extension);
                //if (model.file.Length > 0)
                //{
                //    //dosyamızı kaydediyoruz.
                //    using (var stream = new FileStream(excelPath, FileMode.Create))
                //    {
                //        await model.file.CopyToAsync(stream);
                //    }
                //}
                //TempData["FileGeneralUpdateStatus"] = "true";
                //#endregion
                //var fileUploadResult = _fileColumnService.LoadDataFromExcel(
                //    new FileUploadViewModel
                //    {
                //        file = model.file,
                //        xlsFileName = model.file.FileName,
                //        folderName = $"{categoryNameFolder}",
                //        xlsPath = $"{excelPath}",
                //        fileUploadTypeID = 132,
                //        tableName = "StaffTrackingData"
                //    });
                //if (!fileUploadResult.IsSuccess)
                //{
                //    //databsaeden de sil exceli


                //    System.IO.File.Delete(excelPath);
                //}
                
            }
            TempData["FileGeneralUpdateStatus"] = "false";
            return View("Index");
        }
    }
}