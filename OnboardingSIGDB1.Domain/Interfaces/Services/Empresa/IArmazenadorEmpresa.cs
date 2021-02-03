using OnboardingSIGDB1.Domain.DTOs;
using System;

namespace OnboardingSIGDB1.Domain.Interfaces.Services.Empresa
{
    public interface IArmazenadorEmpresa : IDisposable
    {
        void Add(EmpresaDTO empresa);

        void Update(EmpresaDTO empresa);
    }
}
