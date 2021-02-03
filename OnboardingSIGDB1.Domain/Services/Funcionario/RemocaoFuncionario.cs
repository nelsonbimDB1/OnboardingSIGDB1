using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces.Notification;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Services.Funcionario;
using OnboardingSIGDB1.Domain.Interfaces.UoW;

namespace OnboardingSIGDB1.Domain.Services
{
    public class RemocaoFuncionario : BaseService<int, Funcionario>, IRemocaoFuncionario
    {
        private readonly IFuncionarioRepository _repository;
        public RemocaoFuncionario(IDomainNotificationHandler notification, IUnitOfWork UoW, IFuncionarioRepository repository) : base(notification, UoW) 
        {
            _repository = repository;
        }

        public void Remove(int funcionarioId)
        {
            _repository.Remove(funcionarioId);
            _UoW.Commit();
        }
    }
}
