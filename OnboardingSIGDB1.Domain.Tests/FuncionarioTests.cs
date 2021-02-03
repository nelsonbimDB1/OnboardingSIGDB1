using Xunit;
using System.Linq;
using OnboardingSIGDB1.Domain.Entities;
using System;
using OnboardingSIGDB1.Domain.Notification;

namespace OnboardingSIGDB1.Domain.Tests
{
    public class FuncionarioTests
    {
        public FuncionarioTests() { }

        [Fact]
        public void VerificaNomePreenchido()
        {
            var funcionario = FuncionarioMockData.VerificaNomePreenchidoMockData();

            funcionario.DefineRules();
            AddValidationResult(funcionario);

            Assert.True(funcionario.ValidationResult.Where(p => p.Value == "Nome deve ser preenchido.").Any());
        }

        [Fact]
        public void VerificaNomeTamanho()
        {
            var funcionario = FuncionarioMockData.VerificaNomeTamanhoMockData();

            funcionario.DefineRules();
            AddValidationResult(funcionario);

            Assert.True(funcionario.ValidationResult.Where(p => p.Value == "Nome tem tamanho máximo de 150 caracteres.").Any());
        }

        [Fact]
        public void VerificaCPFPreenchido()
        {
            var funcionario = FuncionarioMockData.VerificaCNPJPreenchidoMockData();

            funcionario.DefineRules();
            AddValidationResult(funcionario);

            Assert.True(funcionario.ValidationResult.Where(p => p.Value == "CPF deve ser preenchido.").Any());
        }

        [Fact]
        public void VerificaCPFTamanho()
        {
            var funcionario = FuncionarioMockData.VerificaCNPJTamanhoMockData();

            funcionario.DefineRules();
            AddValidationResult(funcionario);

            Assert.True(funcionario.ValidationResult.Where(p => p.Value == "CPF deve conter 11 caracteres.").Any());
        }

        [Fact]
        public void VerificaCPFInvalido()
        {
            var funcionario = FuncionarioMockData.VerificaCNPJInvalidoMockData();

            funcionario.DefineRules();
            AddValidationResult(funcionario);

            Assert.True(funcionario.ValidationResult.Where(p => p.Value == "CPF inválido.").Any());
        }

        private static class FuncionarioMockData
        {
            public static Funcionario VerificaNomePreenchidoMockData() => new Funcionario(null, "424.551.158-36", DateTime.Now);
            public static Funcionario VerificaNomeTamanhoMockData() => new Funcionario("Nome".PadLeft(300, 'r'), "424.551.158-36", DateTime.Now);
            public static Funcionario VerificaCNPJPreenchidoMockData() => new Funcionario("Nome", null, DateTime.Now);
            public static Funcionario VerificaCNPJTamanhoMockData() => new Funcionario("Nome", "CPF".PadLeft(50, 'r'), DateTime.Now);
            public static Funcionario VerificaCNPJInvalidoMockData() => new Funcionario("Nome", "444.444.444-44", DateTime.Now);
        }

        private void AddValidationResult(Funcionario entity)
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
