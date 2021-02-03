using System;

namespace OnboardingSIGDB1.Domain.Interfaces.Services.Empresa
{
    public interface IRemocaoEmpresa : IDisposable
    {
        void Remove(int empresaId);
    }
}
