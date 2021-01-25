using System;

namespace OnboardingSIGDB1.Domain.QueryResults
{
    public class FuncionarioQueryResult
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime? DataContratacao { get; set; }
        public CargoFuncionarioQueryResult Cargo { get; set; }
    }

    public class CargoFuncionarioQueryResult
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataVinculo { get; set; }
    }
}
