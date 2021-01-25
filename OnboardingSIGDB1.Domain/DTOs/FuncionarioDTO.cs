using System;

namespace OnboardingSIGDB1.Domain.DTOs
{
    public class FuncionarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime? DataContratacao { get; set; }
    }
}
