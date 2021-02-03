using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.DTOs;
using OnboardingSIGDB1.Domain.Filters;
using OnboardingSIGDB1.Domain.Interfaces.Notification;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Services.Funcionario;

namespace OnboardingSIGDB1.API.Controllers
{
    [Route("api/funcionarios")]
    [ApiController]
    public class FuncionarioController : BaseController
    {
        private readonly IFuncionarioRepository _repository;
        private readonly IArmazenadorFuncionario _armazenador;
        private readonly IRemocaoFuncionario _remocao;

        public FuncionarioController(IDomainNotificationHandler notification, IFuncionarioRepository repository, IArmazenadorFuncionario armazenador, IRemocaoFuncionario remocao, IMapper mapper) : base(notification, mapper)
        {
            _repository = repository;
            _armazenador = armazenador;
            _remocao = remocao;
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
                return Response();
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
            {
                _notification.Adicionar("Id não informado.");
                return Response();
            }

            return Response(_repository.GetByIdCargo(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] FuncionarioDTO dto)
        {
            _armazenador.Add(dto);

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

            _armazenador.Update(dto);

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

            _armazenador.AddEmpresa(id, empresaId);

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

            _armazenador.AddCargo(id, cargoId);

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

            _remocao.Remove(id);

            return Response();
        }
    }
}
