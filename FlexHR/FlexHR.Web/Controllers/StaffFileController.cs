using AutoMapper;
using FlexHR.Business.Interface;
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

namespace FlexHR.Web.Controllers
{
    public class StaffFileController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IStaffFileService _staffFileService;
        private readonly IStaffService _staffService;
        public StaffFileController(IMapper mapper, IGeneralSubTypeService generalSubTypeService, IStaffFileService staffFileService, IConfiguration configuration, IStaffService staffService)
        {
            _mapper = mapper;
            _generalSubTypeService = generalSubTypeService;
            _staffFileService = staffFileService;
            _configuration = configuration;
            _staffService = staffService;
        }
        public static string ClearTurkishCharacter(string _dirtyText)
        {
            var text = _dirtyText;
            var unaccentedText = String.Join("", text.Normalize(NormalizationForm.FormD).Where(c => char.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark));
            return unaccentedText.Replace("ı", "i");
        }
        public IActionResult Index(int id)
        {

            ViewBag.StaffId = id;
            ViewBag.FileTypes = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.FileType), "GeneralSubTypeId", "Description");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStaffFile(IFormFile file, int id, int categoryId)
        {

            if (file != null)
            {
                //int count = 0;
                var staffName = "Staff_" + id;
                string categoryNameFolder;
                var fullPath = _configuration.GetValue<string>("FullPath:DefaultPath");
                var fileTypeList = _generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.FileType);
                foreach (var item in fileTypeList)
                {
                    if (item.GeneralSubTypeId == categoryId)
                    {

                        //string uzanti = Path.GetExtension(item.FileName);
                        //string imageName = Guid.NewGuid() + uzanti;
                        //Dosyamızın kaydedileceği Klasörün yolunu belirliyoruz.
                        categoryNameFolder = ClearTurkishCharacter(item.Description);


                        //var userFileList = _staffFileService.Get(x => x.StaffId == id && x.IsActive == true);
                        //foreach (var userFile in userFileList)
                        //{
                        //    var x = userFile.FileFullPath + userFile.FileName;
                        //    var img = staffName + "\\" + categoryNameFolder + "/" + file.FileName;
                        //    if (img == x)
                        //    {
                        //        count++;
                        //    }

                        //}
                        //if (count == 0)
                        //{
                        var stafFileResult = _staffFileService.AddResult(new StaffFile()
                        {
                            FileFullPath = Path.Combine(staffName, categoryNameFolder + "/"),
                            FileName = file.FileName,
                            FileGeneralSubTypeId = categoryId,
                            IsActive = true,
                            StaffId = id
                        });
                        stafFileResult.FileName = stafFileResult.StaffFileId + stafFileResult.FileName;
                        _staffFileService.Update(stafFileResult);
                        var folderPath = Path.Combine(fullPath, staffName);
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        var filePath = Path.Combine(folderPath, categoryNameFolder + "/");
                        if (!Directory.Exists(filePath))
                            Directory.CreateDirectory(filePath);
                        var imagePath = Path.Combine(filePath + stafFileResult.StaffFileId + file.FileName);
                        if (file.Length > 0)
                        {
                            //dosyamızı kaydediyoruz.
                           
                            using (var stream = new FileStream(imagePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                        }

                        //}
                        //count = 0;

                    }
                }

                return Json(true);
            }
            return Json(true);

        }
        public JsonResult GetStaffFile(int id, int categoryId2)
        {
            var fileList = JsonConvert.SerializeObject(_staffFileService.Get(x => x.StaffId == id && x.FileGeneralSubTypeId == categoryId2 && x.IsActive == true));

            return Json(fileList);
        }
        [HttpPost]
        public JsonResult StaffFileRemove(IFormFile file, int fileId)
        {
            if (fileId > 0)
            {
                var result = _staffFileService.Get(x => x.StaffFileId == fileId).FirstOrDefault();
                result.IsActive = false;
                _staffFileService.Update(result);
                return Json("True");
            }
            return Json("False");
        }
    }
}
