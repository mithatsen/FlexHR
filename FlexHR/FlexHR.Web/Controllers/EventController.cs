using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.EventDtos;
using FlexHR.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IStaffPersonelInfoService _personelInfoService;
        private readonly IMapper _mapper;
        private readonly IPublicHolidayService _publicHolidayService;

        public EventController(IEventService eventService, IMapper mapper, IStaffPersonelInfoService personelInfoService, IPublicHolidayService publicHolidayService)
        {
            _eventService = eventService;
            _mapper = mapper;
            _personelInfoService = personelInfoService;
            _publicHolidayService = publicHolidayService;
        }

        public IActionResult Index()
        {
            ViewBag.EventUpdateStatus = TempData["EventUpdateStatus"] != null ? TempData["EventUpdateStatus"] : "false";
            ViewBag.EventAddStatus = TempData["EventAddStatus"] != null ? TempData["EventAddStatus"] : "false";
            return View();
        }
        public IActionResult GetAllEvents()
        {
            List<ListEventDto> models = new List<ListEventDto>();
            var events = _eventService.Get(p => p.IsActive == true);
            var birthDates = _personelInfoService.Get(p => p.IsActive == true,null,"Staff");
            var publicDays=_publicHolidayService.Get(p => p.IsActive == true);
            foreach (var item in events)
            {
                ListEventDto model = new ListEventDto()
                {
                    Description = item.Description,
                    ClassName = "fc-event-danger fc-event-solid-danger",
                    End = item.End,
                    Start = item.Start,
                    Title = item.Title,
                    IsActive = item.IsActive,
                    Id = item.Id,
                    Editable=true,
                    AllDay=true
                };
                models.Add(model);
            }
            foreach (var item in birthDates)
            {
                var staffName = item.Staff.NameSurname;
                var month = item.BirthDate.Value.Month;
                var day = item.BirthDate.Value.Day;
                var birthDayStart = new DateTime(2021, month, day, 0, 0, 0);
                var birthDayEnd = new DateTime(2021, month, day, 23, 59, 59);
                ListEventDto model = new ListEventDto()
                {
                    Description = staffName + "'ın Doğum Günü",
                    ClassName = "fc-event-success fc-event-solid-success",
                    Start = birthDayStart,
                    Title = "Doğum Günü",
                    IsActive = item.IsActive,
                    AllDay = birthDayEnd.Hour- birthDayStart.Hour >= 23 ? true : false,
                    Editable=false
                };
                models.Add(model);
            }
            foreach (var item in publicDays)
            {
                //var staffName = item.Staff.NameSurname;
                //var month = item.BirthDate.Value.Month;
                //var day = item.BirthDate.Value.Day;
                //var birthDayStart = new DateTime(2021, month, day, 0, 0, 0);
                //var birthDayEnd = new DateTime(2021, month, day, 23, 59, 59); && (item.End.Day - item.Start.Day >=1)
                ListEventDto model = new ListEventDto()
                {
                    Description = item.Description,
                    ClassName = "fc-event-primary fc-event-solid-primary",
                    End = item.End,
                    Start = item.Start,
                    Title = item.Title,
                    IsActive = item.IsActive,
                    Id = item.PublicHolidayId,
                    AllDay = item.IsHalfDay
                };
                models.Add(model);
            }
            return Json(models);
        }

        [HttpPost]
        public IActionResult AddPublicHoliday(AddPublicHolidayDto model)
        {

            if (ModelState.IsValid)
            {
              
              
                model.IsActive = true;
                if (model.IsHalfDay == false)
                {
                    model.End = model.Start.AddHours(23);
                }
                else
                {
                    model.End = model.Start.AddHours(12);
                    model.Description += " (Yarım Gün)";
                }
             
                _publicHolidayService.Add(_mapper.Map<PublicHoliday>(model));
               // TempData["EventAddStatus"] = "true";
            }

            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult UpdatePublicHoliday(ListEventDto model)
        {

            if (ModelState.IsValid)
            {
                _publicHolidayService.Update(_mapper.Map<PublicHoliday>(model));
                TempData["EventUpdateStatus"] = "true";
            }

            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult AddEvent(AddEventDto model)
        {

            if (ModelState.IsValid)
            {
                _eventService.Add(_mapper.Map<Event>(model));
                TempData["EventAddStatus"] = "true";
            }

            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult UpdateEvent(ListEventDto model)
        {

            if (ModelState.IsValid)
            {
                _eventService.Update(_mapper.Map<Event>(model));
                TempData["EventUpdateStatus"] = "true";
            }

            return RedirectToAction("Index");

        }
        [HttpPost]
        public bool DeleteEvent(int id)
        {

            var result = _eventService.GetById(id);
            result.IsActive = false;
            try
            {
                _eventService.Update(result);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
