using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.JobRotationDtos;
using FlexHR.DTO.Dtos.StaffDtos;
using FlexHR.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class JobRotationController : Controller
    {
        private readonly IJobRotationService _jobRotationService;
        private readonly IStaffService _staffService;
        private readonly IMapper _mapper;

        public JobRotationController(IJobRotationService jobRotationService, IMapper mapper, IStaffService staffService)
        {
            _jobRotationService = jobRotationService;
            _mapper = mapper;
            _staffService = staffService;
        }

        public IActionResult Index()
        {
            ViewBag.JobRotationUpdateStatus = TempData["JobRotationUpdateStatus"];
            var result = _jobRotationService.GetAll();
            return View(_mapper.Map<List<ListJobRotationDto>>(result));
        }
        public IActionResult JobRotationDescribing()
        {
            ViewBag.JobRotationList = new SelectList(_jobRotationService.GetAll(), "Id", "Name");
            ViewBag.PersonalList = new SelectList(_staffService.GetAll(), "StaffId", "NameSurname");
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
            }
            _staffService.SaveChanges();
            return RedirectToAction("JobRotationDescribing");
        } 
        public IActionResult GetStaffByRotationId(int id)
        {
            var result=_staffService.Get(x => x.JobRotationId == id && x.IsActive == true).ToList();
            return PartialView("_GetJobRotationTable", _mapper.Map<List<ListStaffJobRotationDto>>(result));
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
        public IActionResult GetJobRotationSelectbox()
        {
            var jobRotation = _staffService.GetAll();
            return Json(jobRotation);
        }   
        //public IActionResult GetJobRotationSelectbox(int id)
        //{
        //    var jobRotation = _staffService.Get(x=>x.JobRotationId==id && x.IsActive==true);
        //    return Json(jobRotation);
        //}
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
