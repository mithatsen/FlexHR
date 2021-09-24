using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.JobRotationDtos;
using FlexHR.DTO.Dtos.StaffDtos;
using FlexHR.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FlexHR.Web.StringInfo.RoleInfo;

namespace FlexHR.Web.Controllers
{
    public class JobRotationController : Controller
    {
        private readonly IJobRotationService _jobRotationService;
        private readonly IJobRotationHistoryService _jobRotationHistoryService;
        private readonly IStaffService _staffService;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IMapper _mapper;

        public JobRotationController(IJobRotationService jobRotationService, IMapper mapper, IStaffService staffService, IGeneralSubTypeService generalSubTypeService, IJobRotationHistoryService jobRotationHistoryService)
        {
            _jobRotationService = jobRotationService;
            _mapper = mapper;
            _generalSubTypeService = generalSubTypeService;
            _staffService = staffService;
            _jobRotationHistoryService = jobRotationHistoryService;
        }
        [Authorize(Roles = "ViewJobRotationSidebar,Manager")]
        public IActionResult Index()
        {
            TempData["Active"] = TempdataInfo.AddJobRotation;
            ViewBag.JobRotationUpdateStatus = TempData["JobRotationUpdateStatus"];
            var result = _jobRotationService.GetAll();
            return View(_mapper.Map<List<ListJobRotationDto>>(result));
        }
        [Authorize(Roles = "ViewDescribingJobRotationPage,Manager")]
        public IActionResult JobRotationDescribing()
        {
            TempData["Active"] = TempdataInfo.AssignJobRotation;
            ViewBag.JobRotationList = new SelectList(_jobRotationService.GetAll(), "Id", "Name");
            ViewBag.PersonalList = new SelectList(_staffService.GetAll(), "StaffId", "NameSurname");
            ViewBag.JobRotationAddStatus = TempData["JobRotationAddStatus"];
            return View();
        }
        [HttpPost]
        public IActionResult JobRotationAssignStaff(JobRotationAssignStaffDto model)
        {
            var staffIdList = model.StaffList;
            var jobRotation = _staffService.Get(x => x.JobRotationId == model.JobRotationId).ToList();
            foreach (var item in jobRotation)
            {
                item.JobRotationId = null;
                _staffService.UpdateNotSave(item);
            }
            foreach (var item in staffIdList)
            {
                var result = _staffService.GetById(item);
                result.JobRotationId = model.JobRotationId;
                _staffService.UpdateNotSave(result);
                ListJobRotationHistoryDto historyDto = new ListJobRotationHistoryDto
                {
                    JobRotationDate = DateTime.Now,
                    JobRotationId = model.JobRotationId,
                    StaffId = item
                };
                _jobRotationHistoryService.Add(_mapper.Map<JobRotationHistory>(historyDto));
            }
            _staffService.SaveChanges();
            TempData["JobRotationAddStatus"] = "true";
            return RedirectToAction("JobRotationDescribing");
        }
        public IActionResult GetStaffByRotationId(int id)
        {
            var result = _staffService.Get(x => x.JobRotationId == id && x.IsActive == true, null, "StaffCareer").ToList();
            List<ListStaffJobRotationDto> models = new List<ListStaffJobRotationDto>();
            foreach (var item in result)
            {

                ListStaffJobRotationDto model = new ListStaffJobRotationDto
                {
                    DepartmentName = item.StaffCareer.FirstOrDefault() != null ? _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.StaffCareer.FirstOrDefault().DepartmantGeneralSubTypeId) : "-",
                    NameSurname = item.NameSurname,
                    PersonalNo = item.PersonalNo,
                    StaffId = item.StaffId
                };
                models.Add(model);
            }
            return PartialView("_GetJobRotationTable", models);
        }
        [HttpPost]
        public IActionResult AddJobRotationWithAjax(ListJobRotationDto model)
        {
            if (ModelState.IsValid)
            {
                _jobRotationService.Add(_mapper.Map<JobRotation>(model));
                return Json("true");
            }
            return Json("false");
        }
        public IActionResult GetUpdateJobRotationModal(int id)
        {
            var jobRotation = _jobRotationService.GetById(id);
            return PartialView("_GetUpdateJobRotationModal", _mapper.Map<ListJobRotationDto>(jobRotation));
        }
        public IActionResult GetJobRotationSelectbox(int id)
        {
            var jobRotation = _staffService.Get(x => x.IsActive == true, null, "StaffCareer").ToList();
            List<ListStaffJobRotationDto> models = new List<ListStaffJobRotationDto>();
            foreach (var item in jobRotation)
            {
                if (item.JobRotationId == id)
                {
                    ListStaffJobRotationDto model = new ListStaffJobRotationDto
                    {
                        DepartmentName = item.StaffCareer.FirstOrDefault() != null ? _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.StaffCareer.FirstOrDefault().DepartmantGeneralSubTypeId) : "-",
                        NameSurname = item.NameSurname,
                        PersonalNo = item.PersonalNo,
                        StaffId = item.StaffId,
                        JobRotationId = item.JobRotationId
                    };
                    models.Add(model);
                }
                if (item.JobRotationId == null)
                {
                    ListStaffJobRotationDto model = new ListStaffJobRotationDto
                    {
                        DepartmentName = item.StaffCareer.FirstOrDefault() != null ? _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.StaffCareer.FirstOrDefault().DepartmantGeneralSubTypeId) : "-",
                        NameSurname = item.NameSurname,
                        PersonalNo = item.PersonalNo,
                        StaffId = item.StaffId,
                        JobRotationId = item.JobRotationId
                    };
                    models.Add(model);
                }
            }
            return Json(models);
        }

        [HttpPost]
        public IActionResult UpdateJobRotation(ListJobRotationDto model)
        {
            if (ModelState.IsValid)
            {
                _jobRotationService.Update(_mapper.Map<JobRotation>(model));
                TempData["JobRotationUpdateStatus"] = "true";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public bool DeleteJobRotation(int id)
        {
            try
            {
                var rotate = _jobRotationService.GetById(id);
                rotate.IsActive = false;
                _jobRotationService.Update(rotate);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteAllRotation()
        {
            try
            {
                var rotate = _staffService.GetAll();
                foreach (var item in rotate)
                {
                    item.JobRotationId = null;
                    _staffService.UpdateNotSave(item);
                }
                _staffService.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
