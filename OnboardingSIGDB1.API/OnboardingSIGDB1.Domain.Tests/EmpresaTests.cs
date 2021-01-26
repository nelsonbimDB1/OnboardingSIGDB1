using Xunit;
using System.Linq;
using OnboardingSIGDB1.Domain.Entities;
using System;

namespace OnboardingSIGDB1.Domain.Tests
{
    public class EmpresaTests
    {
        public EmpresaTests() { }

        [Fact]
        public void VerificaNomePreenchido()
        {
            var empresa = EmpresaMockData.VerificaNomePreenchidoMockData();

            empresa.DefineRules();
            empresa.AddValidationResult(empresa);

            Assert.True(empresa.ValidationResult.Where(p => p.Value == "Nome deve ser preenchido.").Any());
        }

        [Fact]
        public void VerificaNomeTamanho()
        {
            var empresa = EmpresaMockData.VerificaNomeTamanhoMockData();

            empresa.DefineRules();
            empresa.AddValidationResult(empresa);

            Assert.True(empresa.ValidationResult.Where(p => p.Value == "Nome tem tamanho máximo de 150 caracteres.").Any());
        }

        [Fact]
        public void VerificaCNPJPreenchido()
        {
            var empresa = EmpresaMockData.VerificaCNPJPreenchidoMockData();

            empresa.DefineRules();
            empresa.AddValidationResult(empresa);

            Assert.True(empresa.ValidationResult.Where(p => p.Value == "CNPJ deve ser preenchido.").Any());
        }

        [Fact]
        public void VerificaCNPJTamanho()
        {
            var empresa = EmpresaMockData.VerificaCNPJTamanhoMockData();

            empresa.DefineRules();
            empresa.AddValidationResult(empresa);

            Assert.True(empresa.ValidationResult.Where(p => p.Value == "CNPJ deve conter 11 caracteres.").Any());
        }

        [Fact]
        public void VerificaCNPJInvalido()
        {
            var empresa = EmpresaMockData.VerificaCNPJInvalidoMockData();

            empresa.DefineRules();
            empresa.AddValidationResult(empresa);

            Assert.True(empresa.ValidationResult.Where(p => p.Value == "CNPJ inválido.").Any());
        }

        private static class EmpresaMockData
        {
            public static Empresa VerificaNomePreenchidoMockData() => new Empresa(null, "04.204.018/0001-66", DateTime.Now);
            public static Empresa VerificaNomeTamanhoMockData() => new Empresa("Nome".PadLeft(300, 'r'), "04.204.018/0001-66", DateTime.Now);
            public static Empresa VerificaCNPJPreenchidoMockData() => new Empresa("Nome", null, DateTime.Now);
            public static Empresa VerificaCNPJTamanhoMockData() => new Empresa("Nome", "CNPJ".PadLeft(50, 'r'), DateTime.Now);
            public static Empresa VerificaCNPJInvalidoMockData() => new Empresa("Nome", "99999999999999", DateTime.Now);
        }
    }
}
