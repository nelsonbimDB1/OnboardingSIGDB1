using System;
using System.Collections.Generic;
using FluentValidation;

namespace OnboardingSIGDB1.Domain.Entities
{
    public class Cargo : BaseEntity<int, Cargo>
    {
        public string Descricao { get; private set; }
        public List<FuncionarioCargo> FuncionariosCargos { get; private set; } = new List<FuncionarioCargo>();

        public Cargo(string descricao)
        {
            Descricao = descricao;
        }

        protected Cargo() { }

        public void AlteraDescricao(string descricao)
        {
            Descricao = descricao;
        }

        public void InsereFuncionarioCargo(int funcionarioId)
        {
            FuncionariosCargos.Add(new FuncionarioCargo(funcionarioId, Id, DateTime.Now));
        }

        public override void DefineRules()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Descrição deve ser preenchida.")
                .MaximumLength(250).WithMessage("Descrição tem tamanho máximo de 250 caracteres.");
        }
    }
}
