using System;

namespace OnboardingSIGDB1.Domain.Interfaces.Services.Cargo
{
    public interface IRemocaoCargo : IDisposable
    {
        void Remove(int cargoId);
    }
}
