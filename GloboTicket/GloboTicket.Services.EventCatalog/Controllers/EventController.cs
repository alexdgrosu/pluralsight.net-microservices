using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GloboTicket.Services.EventCatalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GloboTicket.Services.EventCatalog.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _repository;
        private readonly IMapper _mapper;

        public EventController(IEventRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Event>>> Get([FromQuery] Guid categoryId)
        {
            var result = await _repository.GetEvents(categoryId);
            return Ok(_mapper.Map<List<Models.Event>>(result));
        }

        [HttpGet("{eventId}")]
        public async Task<ActionResult<Models.Event>> GetById(Guid eventId)
        {
            var result = await _repository.GetEventById(eventId);
            return Ok(_mapper.Map<Models.Event>(result));
        }
    }
}
