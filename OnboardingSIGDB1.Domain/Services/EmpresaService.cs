using OnboardingSIGDB1.Domain.DTOs;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces.Notification;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Services;
using OnboardingSIGDB1.Domain.Interfaces.UoW;

namespace OnboardingSIGDB1.Domain.Services
{
    public class EmpresaService : BaseService<int, Empresa>, IEmpresaService
    {
        private readonly IEmpresaRepository _repository;
        public EmpresaService(IDomainNotificationHandler notification, IUnitOfWork UoW, IEmpresaRepository repository) : base(notification, UoW) 
        {
            _repository = repository;
        }

        public void Add(EmpresaDTO empresadto)
        {
            var empresa = new Empresa(empresadto.Nome, empresadto.CNPJ, empresadto.DataFundacao);

            Manipulate(empresa, _repository.Add);
        }

        public void Update(EmpresaDTO empresadto)
        {
            var empresa = _repository.GetById(empresadto.Id);

            if (empresa == null)
            {
                Notification.Adicionar("Empresa não encontrada.");
                return;
            }

            empresa.AlteraCNPJ(empresadto.CNPJ);
            empresa.AlteraNome(empresadto.Nome);
            empresa.AlteraDataFundacao(empresadto.DataFundacao);

            Manipulate(empresa, _repository.Update);
        }

        public void Remove(int empresaId)
        {
            var cargo = _repository.GetById(empresaId);
            if (cargo?.Funcionarios?.Count > 0)
            {
                Notification.Adicionar("Não foi possível remover a empresa, pois a mesma possui vínculos com funcionários ativos no sistema.");
                return;
            }
            else
            {
                _repository.Remove(empresaId);
                _UoW.Commit();
            }
        }
    }
}
