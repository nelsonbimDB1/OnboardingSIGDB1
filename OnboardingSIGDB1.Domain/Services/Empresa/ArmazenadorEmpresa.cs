using OnboardingSIGDB1.Domain.DTOs;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces.Notification;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Services.Empresa;
using OnboardingSIGDB1.Domain.Interfaces.UoW;

namespace OnboardingSIGDB1.Domain.Services
{
    public class ArmazenadorEmpresa : BaseService<int, Empresa>, IArmazenadorEmpresa
    {
        private readonly IEmpresaRepository _repository;
        public ArmazenadorEmpresa(IDomainNotificationHandler notification, IUnitOfWork UoW, IEmpresaRepository repository) : base(notification, UoW) 
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
    }
}
