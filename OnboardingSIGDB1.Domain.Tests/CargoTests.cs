using Xunit;
using System.Linq;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Notification;

namespace OnboardingSIGDB1.Domain.Tests
{
    public class CargoTests
    {

        public CargoTests()
        { }

        [Fact]
        public void VerificaDescricaoPreenchida()
        {
            var cargo = CargoMockData.VerificaDescricaoPreenchidaMockData();

            cargo.DefineRules();
            AddValidationResult(cargo);

            Assert.True(cargo.ValidationResult.Where(p => p.Value == "Descrição deve ser preenchida.").Any());
        }

        [Fact]
        public void VerificaDescricaoTamanho()
        {
            var cargo = CargoMockData.VerificaDescricaoTamanhoMockData();

            cargo.DefineRules();
            AddValidationResult(cargo);

            Assert.True(cargo.ValidationResult.Where(p => p.Value == "Descrição tem tamanho máximo de 250 caracteres.").Any());
        }

        private static class CargoMockData
        {
            public static Cargo VerificaDescricaoPreenchidaMockData() => new Cargo(null);
            public static Cargo VerificaDescricaoTamanhoMockData() => new Cargo("A".PadLeft(300, 'r'));
        }

        private void AddValidationResult(Cargo entity)
        {
            var results = entity.Validate(entity);
            foreach (var item in results.Errors)
            {
                var propertyName = !string.IsNullOrEmpty(item.PropertyName) ? $"{item.PropertyName}" : null;

                entity.ValidationResult.Add(new DomainNotification($"{propertyName}", item.ErrorMessage));
            }
        }
    }
}
