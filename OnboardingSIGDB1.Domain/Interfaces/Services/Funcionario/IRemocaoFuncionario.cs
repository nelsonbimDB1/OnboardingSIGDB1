using System;

namespace OnboardingSIGDB1.Domain.Interfaces.Services.Funcionario
{
    public interface IRemocaoFuncionario : IDisposable
    {
        void Remove(int funcionarioId);
    }
}
