using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.DTOs;
using OnboardingSIGDB1.Domain.Interfaces.Notification;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Services;
using OnboardingSIGDB1.Domain.QueryResults;

namespace OnboardingSIGDB1.API.Controllers
{
    [Route("api/cargos")]
    [ApiController]
    public class CargoController : BaseController
    {
        private readonly ICargoRepository _repository;
        private readonly ICargoService _service;
        
        public CargoController(IDomainNotificationHandler notification, ICargoRepository repository, ICargoService service, IMapper mapper) : base(notification, mapper)
        {
            _repository = repository;
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Response(_repository.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
            {
                _notification.Adicionar("Id não informado.");
                return BadRequest();
            }

            return Response(_mapper.Map<CargoQueryResult>(_repository.GetById(id)));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CargoDTO dto)
        {
            _service.Add(dto);

            return Response();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, CargoDTO dto)
        {
            if (id == 0 || dto.Id == 0)
            {
                _notification.Adicionar("Id não informado.");
                return BadRequest();
            }

            _service.Update(dto);

            return Response();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                _notification.Adicionar("Id não informado.");
                return BadRequest();
            }

            _service.Remove(id);

            return Response();
        }
    }
}
