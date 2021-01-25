using FluentValidation;
using System;
using System.Collections.Generic;

namespace OnboardingSIGDB1.Domain.Entities
{
    public class Funcionario : BaseEntity<int, Funcionario>
    {
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public DateTime? DataContratacao { get; private set; }
        public int? EmpresaId { get; private set; }
        public Empresa Empresa { get; private set; }
        public List<FuncionarioCargo> FuncionariosCargos { get; private set; }

        public Funcionario(string nome, string cpf, DateTime? dataContratacao)
        {
            Nome = nome;
            CPF = cpf;
            DataContratacao = dataContratacao;
        }

        protected Funcionario() { }

        public void AlteraNome(string nome)
        {
            Nome = nome;
        }

        public void AlteraCPF(string cpf)
        {
            CPF = cpf;
        }

        public void VinculaEmpresa(int empresaId)
        {
            EmpresaId = empresaId;
        }

        public void VinculaCargo(int cargoId)
        {
            if (FuncionariosCargos?.Count > 0)
                FuncionariosCargos.Add(new FuncionarioCargo(Id, cargoId, DateTime.Now));
            else
            {
                FuncionariosCargos = new List<FuncionarioCargo>
                { 
                    new FuncionarioCargo(Id, cargoId, DateTime.Now) 
                };
            }
        }

        public override void DefineRules()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome deve ser preenchido.")
                .MaximumLength(150).WithMessage("Nome tem tamanho máximo de 150 caracteres.");

            RuleFor(x => x.CPF)
                .NotEmpty().WithMessage("CPF deve ser preenchido.")
                .Length(11).WithMessage("CPF deve conter 11 caracteres.")
                .Must(x => ObjectValues.CPF.IsValid(x)).When(x => !string.IsNullOrEmpty(x.CPF))
                .WithMessage("CPF inválido.");

            RuleFor(x => x.DataContratacao)
                .GreaterThanOrEqualTo(DateTime.MinValue).When(x => x.DataContratacao.HasValue)
                .WithMessage("Data inválida.");
        }
    }
}
