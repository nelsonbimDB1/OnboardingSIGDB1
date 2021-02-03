using OnboardingSIGDB1.Domain.DTOs;
using System;

namespace OnboardingSIGDB1.Domain.Interfaces.Services.Cargo
{
    public interface IArmazenadorCargo : IDisposable
    {
        void Add(CargoDTO cargo);

        void Update(CargoDTO cargo);
    }
}
