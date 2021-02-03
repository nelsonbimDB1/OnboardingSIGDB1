using OnboardingSIGDB1.Domain.DTOs;
using System;

namespace OnboardingSIGDB1.Domain.Interfaces.Services.Funcionario
{
    public interface IArmazenadorFuncionario : IDisposable
    {
        void Add(FuncionarioDTO funcionario);

        void Update(FuncionarioDTO funcionario);

        void AddCargo(int funcionarioId, int cargoId);

        void AddEmpresa(int funcionarioId, int empresaId);
    }
}
