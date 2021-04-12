using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GloboTicket.Services.ShoppingBasket.Models;
using GloboTicket.Services.ShoppingBasket.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GloboTicket.Services.ShoppingBasket.Controllers
{
    [ApiController]
    [Route("api/baskets")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{basketId}", Name = "GetBasket")]
        public async Task<ActionResult<Basket>> Get(Guid baskedId)
        {
            var basket = await _repository.GetBasketById(baskedId);

            if (basket is null)
            {
                return NotFound();
            }

            var result = _mapper.Map<Basket>(basket);
            result.NumberOfItems = basket.BasketLines.Sum(bl => bl.TicketAmount);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Basket>> Post(BasketForCreation basket)
        {
            var entity = _mapper.Map<Entities.Basket>(basket);

            _repository.AddBasket(entity);
            await _repository.SaveChanges();

            var model = _mapper.Map<Basket>(entity);

            return CreatedAtRoute(
                "GetBasket",
                new { basketId = entity.BasketId },
                model);
        }
    }
}
