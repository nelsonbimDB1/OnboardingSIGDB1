using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Context;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.QueryResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnboardingSIGDB1.Data.Repositories
{
    public class CargoRepository : ICargoRepository
    {
        private readonly SIGContext _context;
        private readonly IMapper _mapper;
        private bool _disposed;
        public CargoRepository(SIGContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }

                _disposed = true;
            }
        }

        public List<CargoQueryResult> Get() => _mapper.Map<List<CargoQueryResult>>(_context.Cargos.ToList());

        public Cargo GetById(int id) => _context.Cargos.Include(p => p.FuncionariosCargos).Where(p => p.Id == id).FirstOrDefault();

        public void Add(Cargo cargo)
        {
            _context.Cargos.Add(cargo);
        }

        public void Remove(int id) =>
            _context.Cargos.Remove(_context.Cargos.FirstOrDefault(c => c.Id == id));

        public void Update(Cargo cargo)
        {
            _context.Entry(cargo).State = EntityState.Modified;
        }
    }
}
