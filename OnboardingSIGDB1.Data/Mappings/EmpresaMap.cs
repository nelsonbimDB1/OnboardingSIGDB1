using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Data.Mappings
{
    public class EmpresaMap : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(prop => prop.Id)
                   .IsRequired();

            builder.Property(prop => prop.Nome)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(prop => prop.CNPJ)
                   .HasMaxLength(14)
                   .IsRequired();

            builder.Ignore(p => p.ValidationResult);
            builder.Ignore(p => p.CascadeMode);

            builder.Property(prop => prop.DataFundacao);

            builder.HasMany(prop => prop.Funcionarios)
                   .WithOne(prop => prop.Empresa)
                   .HasForeignKey(prop => prop.EmpresaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
