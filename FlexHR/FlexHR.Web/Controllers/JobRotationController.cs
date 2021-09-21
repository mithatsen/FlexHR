using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.JobRotationDtos;
using FlexHR.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class JobRotationController : Controller
    {
        private readonly IJobRotationService _jobRotationService;
        private readonly IMapper _mapper;

        public JobRotationController(IJobRotationService jobRotationService, IMapper mapper)
        {
            _jobRotationService = jobRotationService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            ViewBag.JobRotationUpdateStatus = TempData["JobRotationUpdateStatus"];
            var result = _jobRotationService.GetAll();
            return View(_mapper.Map<List<ListJobRotationDto>>(result));
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
    }
}
