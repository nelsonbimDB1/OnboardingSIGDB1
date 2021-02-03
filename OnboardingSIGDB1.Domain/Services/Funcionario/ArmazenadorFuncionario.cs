using OnboardingSIGDB1.Domain.DTOs;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces.Notification;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Services.Funcionario;
using OnboardingSIGDB1.Domain.Interfaces.UoW;
using System.Linq;

namespace OnboardingSIGDB1.Domain.Services
{
    public class ArmazenadorFuncionario : BaseService<int, Funcionario>, IArmazenadorFuncionario
    {
        private readonly IFuncionarioRepository _repository;
        public ArmazenadorFuncionario(IDomainNotificationHandler notification, IUnitOfWork UoW, IFuncionarioRepository repository) : base(notification, UoW) 
        {
            _repository = repository;
        }

        public void Add(FuncionarioDTO funcionariodto)
        {
            var funcionario = new Funcionario(funcionariodto.Nome, funcionariodto.CPF, funcionariodto.DataContratacao);
            Manipulate(funcionario, _repository.Add);
        }

        public void Update(FuncionarioDTO funcionariodto)
        {
            var funcionario = _repository.GetById(funcionariodto.Id);

            if (funcionario == null)
            {
                Notification.Adicionar("Funcionário não encontrado.");
                return;
            }

            Manipulate(funcionario, _repository.Update);
        }

        public void AddEmpresa(int funcionarioId, int empresaId)
        {            
            var funcionario = _repository.GetById(funcionarioId);

            if (empresaId == 0 && funcionario?.EmpresaId != null)
            {
                Notification.Adicionar("O funcionário não pode se desvincular de uma empresa.");
                return;
            }

            funcionario.VinculaEmpresa(empresaId);
            _UoW.Commit();
        }

        public void AddCargo(int funcionarioId, int cargoId)
        {
            var funcionario = _repository.GetById(funcionarioId);

            if (funcionario?.FuncionariosCargos?.Count > 0 && funcionario.FuncionariosCargos.Any(p => p.CargoId == cargoId))
            {
                Notification.Adicionar("Cargo já vinculado a este funcionário.");
                return;
            }
            else if (funcionario.Empresa == null)
            {
                Notification.Adicionar("Funcionário não vinculado a uma empresa.");
                return;
            }

            funcionario.VinculaCargo(cargoId);
            _UoW.Commit();
        }
    }
}
