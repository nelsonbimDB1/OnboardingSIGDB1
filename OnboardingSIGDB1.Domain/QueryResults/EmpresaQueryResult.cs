using System;

namespace OnboardingSIGDB1.Domain.QueryResults
{
    public class EmpresaQueryResult
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public DateTime? DataFundacao { get; set; }
    }
}
