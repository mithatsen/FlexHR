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

            var model = new FileUploadIndexViewModel();
            model.ColumnList = _fileColumnService.FileColumnListByTypeId(EnumFileType.StaffTrackingFile.GetHashCode());
            return View(model);
        }
        public IActionResult Refectory()
        {
            TempData["Active"] = TempdataInfo.Refectory;
            ViewBag.FileGeneralUpdateStatus = TempData["FileGeneralUpdateStatus"];

            var model = new FileUploadIndexViewModel();
            model.ColumnList = _fileColumnService.FileColumnListByTypeId(EnumFileType.RefectoryFile.GetHashCode());
            return View(model);
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
                    if (fileName == "StaffTracking")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Refectory");
                    }

                }




            }
            TempData["FileGeneralUpdateStatus"] = "false";
            return View("Index");
        }

        [HttpPost]
        public IActionResult LoadDataGenericFileTableList(int id)
        {
            var dict = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
            var draw = dict["draw"];
            var start = dict["start"];
            var length = dict["length"];
            var sortColumn = dict["columns[" + dict["order[0][column]"] + "][name]"];
            var sortColumnDir = dict["order[0][dir]"];
            var searchValue = dict["search[value]"];

            //Paging Size (10,20,50,100)    
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            ////Get StockUnitList
            var dynamicExcelColumnList = _fileColumnService.FileColumnListByTypeId(id);
            var tableName = "";
            if (EnumFileType.StaffTrackingFile.GetHashCode() == id)
            {
                tableName = EnumTableName.StaffTracking.ToString();
            }
            else if (EnumFileType.RefectoryFile.GetHashCode() == id)
            {
                tableName = EnumTableName.Refectory.ToString();
            }
            var dataDB = _fileColumnService.GetStaffTrackkingData(tableName).list;

            var data = new List<List<ReadGenericTableViewModel>>();

            foreach (var row in dataDB)
            {
                var rowData = new List<ReadGenericTableViewModel>();

                foreach (var column in row)
                {
                    rowData.Add(new ReadGenericTableViewModel()
                    {
                        ColumnName = column.ColumnName,
                        Value = column.Value
                    });
                }
                data.Add(rowData);
            }

            ////Sorting    
            //if ((!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    data = data.OrderBy(sortColumn + " " + sortColumnDir).ToList();
            //}

            ////Search    
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(
                    m => m[0].Value.ToLower().Contains(searchValue.ToLower()) ||
                    m[1].Value.ToLower().Contains(searchValue.ToLower()) ||
                    m[2].Value.ToLower().Contains(searchValue.ToLower()) ||
                    m[3].Value.ToLower().Contains(searchValue.ToLower()) ||
                    m[4].Value.ToLower().Contains(searchValue.ToLower())
                    ).ToList();
            }

            //total number of rows count     
            recordsTotal = data.Count();

            //Paging     
            var resultlist = new List<List<string>>();

            foreach (var row in data.Skip(skip).Take(pageSize).ToList())
            {
                var rowData = new List<string>();

                foreach (var column in row)
                {
                    rowData.Add(column.Value);
                }
                resultlist.Add(rowData);
            }


            //Returning Json Data    
            var jsonResult = Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = resultlist });
            //jsonResult.MaxsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}