using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.ViewModels;
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
        public UploadFileController(IConfiguration configuration, IMapper mapper, ICompanyFileService companyFileService, IGeneralSubTypeService generalSubTypeService)
        {
            _configuration = configuration;
            _generalSubTypeService = generalSubTypeService;
            _mapper = mapper;
            _companyFileService = companyFileService;
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
                if (model.CategoryId == 127)
                {
                    fileName = "Refectory";
                }
                else if (model.CategoryId == 129)
                {
                    fileName = "StaffTracking";
                }
                string extension = Path.GetExtension(model.file.FileName);
                string categoryTypeFolder;
                string categoryNameFolder = "Excel";
                categoryTypeFolder = fileName;

                var fullPath = _configuration.GetValue<string>("FullPath:DefaultPath");
                var fileNameUpdate = model.Date.ToString("d") + "_" + fileName + extension;
                var result = _companyFileService.GetAll();
                foreach (var item in result)
                {
                    if (fileNameUpdate == item.FileName)
                    {
                        var folderPathUpdate = Path.Combine(fullPath);
                        if (!Directory.Exists(folderPathUpdate))
                            Directory.CreateDirectory(folderPathUpdate);
                        //Excel klasörü oluşturuldu
                        var filePathUpdate = Path.Combine(folderPathUpdate, categoryNameFolder + "/");
                        if (!Directory.Exists(filePathUpdate))
                            Directory.CreateDirectory(filePathUpdate);
                        //
                        filePathUpdate = Path.Combine(filePathUpdate, categoryTypeFolder + "/");
                        if (!Directory.Exists(filePathUpdate))
                            Directory.CreateDirectory(filePathUpdate);
                        var excelPathUpdate = Path.Combine(filePathUpdate + model.Date.ToString("d") + "_" + fileName + extension);
                        if (model.file.Length > 0)
                        {
                            //dosyamızı kaydediyoruz.
                            using (var stream = new FileStream(excelPathUpdate, FileMode.Create))
                            {
                                await model.file.CopyToAsync(stream);
                            }
                        }
                        TempData["FileGeneralUpdateStatus"] = "true";
                        if (model.CategoryId == 127)
                        {
                            return RedirectToAction("Refectory");
                        }
                        else if (model.CategoryId == 129)
                        {
                            return RedirectToAction("Index");
                        }
                        

                    }
                }
                _companyFileService.Add(new CompanyFile()
                {
                    FileFullPath = Path.Combine(categoryNameFolder + "/" + model.Date.ToString("d") + "_" + fileName + extension),
                    FileName = model.Date.ToString("d") + "_" + fileName + extension,
                    FileGeneralSubTypeId = model.CategoryId,
                    IsActive = true,
                });

                //Flex > Files klasörü oluşturuldu
                var folderPath = Path.Combine(fullPath);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                //Excel klasörü oluşturuldu
                var filePath = Path.Combine(folderPath, categoryNameFolder + "/");
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                //
                filePath = Path.Combine(filePath, categoryTypeFolder + "/");
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                var excelPath = Path.Combine(filePath + model.Date.ToString("d") + "_" + fileName + extension);
                if (model.file.Length > 0)
                {
                    //dosyamızı kaydediyoruz.
                    using (var stream = new FileStream(excelPath, FileMode.Create))
                    {
                        await model.file.CopyToAsync(stream);
                    }
                }
                TempData["FileGeneralUpdateStatus"] = "true";
                return RedirectToAction("Index");
            }
            TempData["FileGeneralUpdateStatus"] = "false";
            return View("Index");
        }
    }
}
