using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.QueryResults;
using System;
using System.Collections.Generic;

namespace OnboardingSIGDB1.Domain.Interfaces.Repositories
{
    public interface ICargoRepository : IDisposable
    {
        List<CargoQueryResult> Get();
        Cargo GetById(int id);
        void Add(Cargo cargo);
        void Remove(int id);
        void Update(Cargo cargo);
    }
}
