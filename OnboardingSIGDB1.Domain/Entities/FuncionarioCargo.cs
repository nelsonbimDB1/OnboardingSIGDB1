using System;

namespace OnboardingSIGDB1.Domain.Entities
{
    public class FuncionarioCargo
    {
        public int FuncionarioId { get; private set; }
        public int CargoId { get; private set; }
        public DateTime DataVinculo { get; private set; }
        public Cargo Cargo { get; private set; }
        public Funcionario Funcionario { get; private set; }

        public FuncionarioCargo(int funcionarioId, int cargoId, DateTime dataVinculo)
        {
            FuncionarioId = funcionarioId;
            CargoId = cargoId;
            DataVinculo = dataVinculo;
        }

        protected FuncionarioCargo() { }
    }
}
