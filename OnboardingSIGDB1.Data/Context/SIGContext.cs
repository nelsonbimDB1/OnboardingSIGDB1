using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Mappings;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Data.Context
{
    public class SIGContext : DbContext
    {
        public SIGContext(DbContextOptions<SIGContext> options) : base(options) { }

        #region DBSets

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<FuncionarioCargo> FuncionariosCargos { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Mappings

            modelBuilder.ApplyConfiguration(new CargoMap());
            modelBuilder.ApplyConfiguration(new EmpresaMap());
            modelBuilder.ApplyConfiguration(new FuncionarioMap());
            modelBuilder.ApplyConfiguration(new FuncionarioCargoMap());

            #endregion Mappings

            base.OnModelCreating(modelBuilder);
        }

    }
}
