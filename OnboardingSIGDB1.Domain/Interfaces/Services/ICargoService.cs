using OnboardingSIGDB1.Domain.DTOs;
using System;

namespace OnboardingSIGDB1.Domain.Interfaces.Services
{
    public interface ICargoService : IDisposable
    {
        void Add(CargoDTO cargo);

        void Update(CargoDTO cargo);

        void Remove(int cargoId);
    }
}
