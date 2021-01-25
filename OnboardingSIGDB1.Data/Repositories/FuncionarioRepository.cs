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
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly SIGContext _context;
        private bool _disposed;

        public FuncionarioRepository(SIGContext context)
        {
            _context = context;
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

        public List<FuncionarioQueryResult> Get()
        {
            var funcionarios = _context.Funcionarios.ToList();

            var funcionariosQueryResult = funcionarios.Select(p => new FuncionarioQueryResult
            {
                Id = p.Id,
                Nome = p.Nome,
                CPF = p.CPF,
                DataContratacao = p.DataContratacao,
                Cargo = p.FuncionariosCargos.OrderByDescending(d => d.DataVinculo).FirstOrDefault() == null ? null : new CargoFuncionarioQueryResult
                {
                    Id = p.FuncionariosCargos.OrderByDescending(d => d.DataVinculo).FirstOrDefault().CargoId,
                    Descricao = p.FuncionariosCargos.OrderByDescending(d => d.DataVinculo).FirstOrDefault().Cargo.Descricao,
                    DataVinculo = p.FuncionariosCargos.OrderByDescending(d => d.DataVinculo).FirstOrDefault().DataVinculo
                }
            }).ToList();

            return funcionariosQueryResult;
        }

        public List<FuncionarioQueryResult> GetByFiltro(FuncionarioFilter filter)
        {
            var funcionarios = _context.Funcionarios.Where(p => string.IsNullOrEmpty(filter.Nome) || p.Nome.Contains(filter.Nome) &&
                                                string.IsNullOrEmpty(filter.CPF) || p.CPF == filter.CPF &&
                                                (!filter.DataContratacaoInicio.HasValue && !filter.DataContratacaoFim.HasValue) ||
                                                p.DataContratacao >= filter.DataContratacaoInicio.Value && p.DataContratacao <= filter.DataContratacaoInicio.Value).ToList();

            var funcionariosQueryResult = funcionarios.Select(p => new FuncionarioQueryResult
            {
                Id = p.Id,
                Nome = p.Nome,
                CPF = p.CPF,
                DataContratacao = p.DataContratacao,
                Cargo = p.FuncionariosCargos.OrderByDescending(d => d.DataVinculo).FirstOrDefault() == null ? null : new CargoFuncionarioQueryResult
                {
                    Id = p.FuncionariosCargos.OrderByDescending(d => d.DataVinculo).FirstOrDefault().CargoId,
                    Descricao = p.FuncionariosCargos.OrderByDescending(d => d.DataVinculo).FirstOrDefault().Cargo.Descricao,
                    DataVinculo = p.FuncionariosCargos.OrderByDescending(d => d.DataVinculo).FirstOrDefault().DataVinculo
                }
            }).ToList();

            return funcionariosQueryResult;
        }

        public Funcionario GetById(int id) => _context.Funcionarios.Include(p => p.FuncionariosCargos).Include(p => p.Empresa).Where(p => p.Id == id).FirstOrDefault();

        public void Add(Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
        }

        public void Remove(int id)
        {
            var funcionario = _context.Funcionarios.Include(p => p.FuncionariosCargos).FirstOrDefault(c => c.Id == id);

            if (funcionario.FuncionariosCargos != null && funcionario.FuncionariosCargos.Count > 0)
            {
                foreach (var funcionarioCargo in funcionario.FuncionariosCargos)
                {
                    _context.Entry(funcionarioCargo).State = EntityState.Deleted;
                }
            }
            _context.Funcionarios.Remove(_context.Funcionarios.FirstOrDefault(c => c.Id == id));
        }

        public void Update(Funcionario funcionario) => _context.Entry(funcionario).State = EntityState.Modified;

        public void AddCargo(Funcionario funcionario, int cargoId)
        {
            funcionario.VinculaCargo(cargoId);
            _context.Entry(funcionario).State = EntityState.Modified;

            var funcionarioCargo = funcionario.FuncionariosCargos.Find(p => p.CargoId == cargoId);
            _context.Entry(funcionarioCargo).State = EntityState.Added;
        }

        public void AddEmpresa(Funcionario funcionario, int empresaId)
        {
            funcionario.VinculaEmpresa(empresaId);
            _context.Entry(funcionario).State = EntityState.Modified;
        }
    }
}
