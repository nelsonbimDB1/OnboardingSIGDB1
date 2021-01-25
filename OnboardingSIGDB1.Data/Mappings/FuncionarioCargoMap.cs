using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Data.Mappings
{
    public class FuncionarioCargoMap : IEntityTypeConfiguration<FuncionarioCargo>
    {
        public void Configure(EntityTypeBuilder<FuncionarioCargo> builder)
        {
            builder.HasKey(entity => new { entity.FuncionarioId, entity.CargoId });

            builder.Property(prop => prop.FuncionarioId)
                   .IsRequired();

            builder.Property(prop => prop.CargoId)
                   .IsRequired();

            builder.Property(prop => prop.DataVinculo)
                   .IsRequired();
        }
    }
}
