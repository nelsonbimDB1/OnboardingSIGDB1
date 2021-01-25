using OnboardingSIGDB1.Domain.DTOs;
using System;

namespace OnboardingSIGDB1.Domain.Interfaces.Services
{
    public interface IFuncionarioService : IDisposable
    {
        void Add(FuncionarioDTO funcionario);

        void Update(FuncionarioDTO funcionario);

        void Remove(int funcionarioId);

        void AddCargo(int funcionarioId, int cargoId);

        void AddEmpresa(int funcionarioId, int empresaId);
    }
}
