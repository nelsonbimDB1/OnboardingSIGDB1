using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.DTOs;
using OnboardingSIGDB1.Domain.Filters;
using OnboardingSIGDB1.Domain.Interfaces.Notification;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Services;
using OnboardingSIGDB1.Domain.QueryResults;

namespace OnboardingSIGDB1.API.Controllers
{
    [Route("api/funcionarios")]
    [ApiController]
    public class FuncionarioController : BaseController
    {
        private readonly IFuncionarioRepository _repository;
        private readonly IFuncionarioService _service;
        
        public FuncionarioController(IDomainNotificationHandler notification, IFuncionarioRepository repository, IFuncionarioService service, IMapper mapper) : base(notification, mapper)
        {
            _repository = repository;
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Response(_repository.Get());
        }

        [HttpGet("pesquisar")]
        public IActionResult Get([FromQuery] FuncionarioFilter filter)
        {
            if (filter == null)
                return Response(_repository.Get());

            if (filter.DateTimeValidate())
                return Response(_repository.GetByFiltro(filter));
            else
            {
                _notification.Adicionar("Data para filtro inválida.");
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
            {
                _notification.Adicionar("Id não informado.");
                return BadRequest();
            }

            return Response(_mapper.Map<FuncionarioQueryResult>(_repository.GetById(id)));
        }

        [HttpPost]
        public IActionResult Post([FromBody] FuncionarioDTO dto)
        {
            _service.Add(dto);

            return Response();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, FuncionarioDTO dto)
        {
            if (id == 0 || dto.Id == 0)
            {
                _notification.Adicionar("Id não informado.");
                return BadRequest();
            }

            _service.Update(dto);

            return Response();
        }

        [HttpPut("{id}/vincular-empresa")]
        public IActionResult VincularEmpresa(int id, int empresaId)
        {
            if (id == 0 || empresaId == 0)
            {
                _notification.Adicionar("Id não informado.");
                return BadRequest();
            }

            _service.AddEmpresa(id, empresaId);

            return Response();
        }

        [HttpPut("{id}/vincular-cargo")]
        public IActionResult VincularCargo(int id, int cargoId)
        {
            if (id == 0 || cargoId == 0)
            {
                _notification.Adicionar("Id não informado.");
                return BadRequest();
            }

            _service.AddCargo(id, cargoId);

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
