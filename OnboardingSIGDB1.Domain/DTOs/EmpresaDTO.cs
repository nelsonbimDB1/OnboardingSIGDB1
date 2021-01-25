using System;

namespace OnboardingSIGDB1.Domain.DTOs
{
    public class EmpresaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public DateTime? DataFundacao { get; set; }
    }
}
