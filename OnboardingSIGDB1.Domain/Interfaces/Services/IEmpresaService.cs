using OnboardingSIGDB1.Domain.DTOs;
using System;

namespace OnboardingSIGDB1.Domain.Interfaces.Services
{
    public interface IEmpresaService : IDisposable
    {
        void Add(EmpresaDTO empresa);

        void Update(EmpresaDTO empresa);

        void Remove(int empresaId);
    }
}
