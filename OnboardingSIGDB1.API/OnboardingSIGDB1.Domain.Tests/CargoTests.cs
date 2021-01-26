using OnboardingSIGDB1.Domain.Interfaces.Notification;
using OnboardingSIGDB1.Domain.Interfaces.Services;
using Xunit;
using Moq;
using System.Linq;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Domain.Tests
{
    public class CargoTests
    {

        private readonly Mock<IDomainNotificationHandler> _notification;
        private readonly Mock<ICargoService> _service;

        public CargoTests()
        {
            _notification = new Mock<IDomainNotificationHandler>();
            _service = new Mock<ICargoService>();
        }

        [Fact]
        public void VerificaDescricaoPreenchida()
        {
            var cargo = CargoMockData.VerificaDescricaoPreenchidaMockData();

            cargo.DefineRules();
            cargo.AddValidationResult(cargo);

            Assert.True(cargo.ValidationResult.Where(p => p.Value == "Descrição deve ser preenchida.").Any());
        }

        [Fact]
        public void VerificaDescricaoTamanho()
        {
            var cargo = CargoMockData.VerificaDescricaoTamanhoMockData();

            cargo.DefineRules();
            cargo.AddValidationResult(cargo);

            Assert.True(cargo.ValidationResult.Where(p => p.Value == "Descrição tem tamanho máximo de 250 caracteres.").Any());
        }

        private static class CargoMockData
        {
            public static Cargo VerificaDescricaoPreenchidaMockData() => new Cargo(null);
            public static Cargo VerificaDescricaoTamanhoMockData() => new Cargo("A".PadLeft(300, 'r'));
        }
    }
}
