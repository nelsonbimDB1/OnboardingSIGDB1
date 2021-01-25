using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Context;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Filters;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.QueryResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnboardingSIGDB1.Data.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly SIGContext _context;
        private readonly IMapper _mapper;
        private bool _disposed;
        public EmpresaRepository(SIGContext context, IMapper mapper)
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

        public List<EmpresaQueryResult> Get() => _mapper.Map<List<EmpresaQueryResult>>(_context.Empresas.ToList());

        public List<EmpresaQueryResult> GetByFiltro(EmpresaFilter filter)
        {
            return _mapper.Map<List<EmpresaQueryResult>>(
                _context.Empresas.Where(p => (string.IsNullOrEmpty(filter.Nome) || p.Nome.Contains(filter.Nome)) &&
                                             (string.IsNullOrEmpty(filter.CNPJ) || p.CNPJ == filter.CNPJ) &&
                                             filter.DataFundacaoFim == null && filter.DataFundacaoFim == null ||
                                             p.DataFundacao >= filter.DataFundacaoInicio && p.DataFundacao <= filter.DataFundacaoFim).ToList());
        }

        public Empresa GetById(int id) => _context.Empresas.Where(p => p.Id == id).FirstOrDefault();

        public void Add(Empresa empresa)
        {
            _context.Empresas.Add(empresa);
        }

        public void Remove(int id) =>
            _context.Empresas.Remove(_context.Empresas.FirstOrDefault(c => c.Id == id));

        public void Update(Empresa empresa)
        {
            _context.Entry(empresa).State = EntityState.Modified;
        }
    }
}
