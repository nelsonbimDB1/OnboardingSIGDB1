using OnboardingSIGDB1.Domain.DTOs;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces.Notification;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Services;
using OnboardingSIGDB1.Domain.Interfaces.UoW;

namespace OnboardingSIGDB1.Domain.Services
{
    public class CargoService : BaseService<int, Cargo>, ICargoService
    {
        private readonly ICargoRepository _repository;
        public CargoService(IDomainNotificationHandler notification, IUnitOfWork UoW, ICargoRepository repository) : base(notification, UoW) 
        {
            _repository = repository;
        }

        public void Add(CargoDTO cargoDTO)
        {
            var cargo = new Cargo(cargoDTO.Descricao);

            Manipulate(cargo, _repository.Add);
        }

        public void Update(CargoDTO cargoDTO)
        {
            var cargo = _repository.GetById(cargoDTO.Id);

            if (cargo == null)
            {
                Notification.Adicionar("Cargo não encontrado.");
                return;
            }

            cargo.AlteraDescricao(cargoDTO.Descricao);

            Manipulate(cargo, _repository.Update);
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
