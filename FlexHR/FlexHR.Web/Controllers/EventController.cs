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
        private readonly IMapper _mapper;

        public EventController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;

        }

        public IActionResult Index()
        {
      
            return View();
        }
        public IActionResult GetAllEvents()
        {
            ViewBag.EventUpdateStatus = TempData["EventUpdateStatus"];
            ViewBag.EventAddStatus = TempData["EventAddStatus"];
            List<ListEventDto> models = new List<ListEventDto>();
            var events = _eventService.GetAll();
            foreach (var item in events)
            {
                ListEventDto model = new ListEventDto()
                {
                    Description = item.Description,
                    ClassName = "fc-event-danger fc-event-solid-warning",
                    End = item.End,
                    Start = item.Start,
                    Title = item.Title,
                    IsActive = item.IsActive,
                    Id = item.Id
                };
                models.Add(model);
            }
            return Json(models);
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

            if (ModelState.IsValid)
            {
                _eventService.Delete(id);

            }

            return true;

        }
    }
}
