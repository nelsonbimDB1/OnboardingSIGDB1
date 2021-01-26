using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Filters;
using OnboardingSIGDB1.Domain.QueryResults;
using System;
using System.Collections.Generic;

namespace OnboardingSIGDB1.Domain.Interfaces.Repositories
{
    public interface IFuncionarioRepository : IDisposable
    {
        List<FuncionarioQueryResult> Get();
        List<FuncionarioQueryResult> GetByFiltro(FuncionarioFilter filter);
        Funcionario GetById(int id);
        FuncionarioQueryResult GetByIdCargo(int id);
        void Add(Funcionario funcionario);
        void Remove(int id);
        void Update(Funcionario funcionario);
        void AddCargo(Funcionario funcionario, int cargoId);
        void AddEmpresa(Funcionario funcionario, int empresaId);

    }
}
