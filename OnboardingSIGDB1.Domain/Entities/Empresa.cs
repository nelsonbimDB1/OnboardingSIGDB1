using System;
using System.Collections.Generic;
using FluentValidation;

namespace OnboardingSIGDB1.Domain.Entities
{
    public class Empresa : BaseEntity<int, Empresa>
    {
        public string Nome { get; private set; }
        public string CNPJ { get; private set; }
        public DateTime? DataFundacao { get; private set; }
        public IReadOnlyCollection<Funcionario> Funcionarios { get; private set; }

        public Empresa(string nome, string cnpj, DateTime? dataFundacao)
        {
            Nome = nome;
            CNPJ = ObjectValues.CNPJ.RemoveMask(cnpj);
            DataFundacao = dataFundacao;
        }

        protected Empresa() { }

        public void AlteraNome(string nome) => Nome = nome;

        public void AlteraCNPJ(string cnpj) => CNPJ = ObjectValues.CNPJ.RemoveMask(cnpj);

        public void AlteraDataFundacao(DateTime? dataFundacao) => DataFundacao = dataFundacao;

        public override void DefineRules()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome deve ser preenchido.")
                .MaximumLength(150).WithMessage("Nome tem tamanho máximo de 150 caracteres.");

            RuleFor(x => x.CNPJ)
                .NotEmpty().WithMessage("CNPJ deve ser preenchido.")
                .Length(14).WithMessage("CNPJ deve conter 11 caracteres.")
                .Must(x => ObjectValues.CNPJ.IsValid(x)).When(x => !string.IsNullOrEmpty(x.CNPJ))
                .WithMessage("CNPJ inválido.");

            RuleFor(x => x.DataFundacao)
                .GreaterThanOrEqualTo(DateTime.MinValue).When(x => x.DataFundacao.HasValue)
                .WithMessage("Data inválida.");
        }
    }
}
