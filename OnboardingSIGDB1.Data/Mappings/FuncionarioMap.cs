using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Data.Mappings
{
    public class FuncionarioMap : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(prop => prop.Id)
                   .IsRequired();

            builder.Property(prop => prop.Nome)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(prop => prop.CPF)
                   .HasMaxLength(11)
                   .IsRequired();

            builder.Property(prop => prop.DataContratacao);

            builder.Ignore(p => p.Empresa);

            builder.Ignore(p => p.ValidationResult);
            builder.Ignore(p => p.CascadeMode);

            builder.HasMany(prop => prop.FuncionariosCargos)
                   .WithOne(prop => prop.Funcionario)
                   .HasForeignKey(prop => prop.FuncionarioId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(prop => prop.Empresa)
                   .WithMany(prop => prop.Funcionarios)
                   .HasForeignKey(prop => prop.EmpresaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
