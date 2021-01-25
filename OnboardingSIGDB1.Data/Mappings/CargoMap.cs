using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Data.Mappings
{
    public class CargoMap : IEntityTypeConfiguration<Cargo>
    {
        public void Configure(EntityTypeBuilder<Cargo> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(prop => prop.Id)
                   .IsRequired();

            builder.Property(prop => prop.Descricao)
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Ignore(p => p.ValidationResult);
            builder.Ignore(p => p.CascadeMode);

            builder.HasMany(prop => prop.FuncionariosCargos)
                   .WithOne(prop => prop.Cargo)
                   .HasForeignKey(prop => prop.CargoId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
