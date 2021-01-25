using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Filters;
using OnboardingSIGDB1.Domain.QueryResults;
using System;
using System.Collections.Generic;

namespace OnboardingSIGDB1.Domain.Interfaces.Repositories
{
    public interface IEmpresaRepository : IDisposable
    {
        List<EmpresaQueryResult> Get();
        List<EmpresaQueryResult> GetByFiltro(EmpresaFilter filter);
        Empresa GetById(int id);
        void Add(Empresa empresa);
        void Remove(int id);
        void Update(Empresa empresa);
    }
}
