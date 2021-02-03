using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces.Notification;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Services.Empresa;
using OnboardingSIGDB1.Domain.Interfaces.UoW;

namespace OnboardingSIGDB1.Domain.Services
{
    public class RemocaoEmpresa : BaseService<int, Empresa>, IRemocaoEmpresa
    {
        private readonly IEmpresaRepository _repository;
        public RemocaoEmpresa(IDomainNotificationHandler notification, IUnitOfWork UoW, IEmpresaRepository repository) : base(notification, UoW) 
        {
            _repository = repository;
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
