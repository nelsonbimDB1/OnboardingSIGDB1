using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.DTOs;
using OnboardingSIGDB1.Domain.Filters;
using OnboardingSIGDB1.Domain.Interfaces.Notification;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Services;
using OnboardingSIGDB1.Domain.Interfaces.Services.Empresa;
using OnboardingSIGDB1.Domain.QueryResults;

namespace OnboardingSIGDB1.API.Controllers
{
    [Route("api/empresas")]
    [ApiController]
    public class EmpresaController : BaseController
    {
        private readonly IEmpresaRepository _repository;
        private readonly IArmazenadorEmpresa _armazenador;
        private readonly IRemocaoEmpresa _remocao;

        public EmpresaController(IDomainNotificationHandler notification, IEmpresaRepository repository, IArmazenadorEmpresa armazenador, IRemocaoEmpresa remocao, IMapper mapper) : base(notification, mapper)
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
        public IActionResult Get([FromQuery] EmpresaFilter filter)
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

            return Response(_mapper.Map<EmpresaQueryResult>(_repository.GetById(id)));
        }

        [HttpPost]
        public IActionResult Post([FromBody] EmpresaDTO dto)
        {
            _armazenador.Add(dto);

            return Response();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, EmpresaDTO dto)
        {
            if (id == 0 || dto.Id == 0)
            {
                _notification.Adicionar("Id não informado.");
                return BadRequest();
            }

            _armazenador.Update(dto);

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
