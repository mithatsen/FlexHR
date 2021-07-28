using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.EventDtos;
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

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAllEvents()
        {
            List<ListEventDto> models = new List<ListEventDto>();
            var events = _eventService.GetAll();
            foreach (var item in events)
            {
                ListEventDto model = new ListEventDto()
                {
                    Description=item.Description,
                    ClassName= "fc-event-danger fc-event-solid-warning",
                    End=item.End,
                    Start=item.Start,
                    Title=item.Title,
                    IsActive=item.IsActive,
                    Id=item.Id
                };
                models.Add(model);
            }
            return Json(models);
        }
    }
}
