using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces.Notification;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Services.Cargo;
using OnboardingSIGDB1.Domain.Interfaces.UoW;

namespace OnboardingSIGDB1.Domain.Services
{
    public class RemocaoCargo : BaseService<int, Cargo>, IRemocaoCargo
    {
        private readonly ICargoRepository _repository;
        public RemocaoCargo(IDomainNotificationHandler notification, IUnitOfWork UoW, ICargoRepository repository) : base(notification, UoW) 
        {
            _repository = repository;
        }

        public void Remove(int cargoId)
        {
            var cargo = _repository.GetById(cargoId);

            if (cargo == null)
            {
                Notification.Adicionar("Cargo não encontrado.");
                return;
            }

            if (cargo?.FuncionariosCargos?.Count > 0)
            {
                Notification.Adicionar("Não foi possível remover o cargo, pois o mesmo possui vínculos com funcionários ativos no sistema.");
                return;
            }
            else
            {
                _repository.Remove(cargoId);
                _UoW.Commit();
            }
        }
    }
}
