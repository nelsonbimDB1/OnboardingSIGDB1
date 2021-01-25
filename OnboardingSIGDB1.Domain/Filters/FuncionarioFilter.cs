using System;

namespace OnboardingSIGDB1.Domain.Filters
{
    public class FuncionarioFilter
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime? DataContratacaoInicio { get; set; }
        public DateTime? DataContratacaoFim { get; set; }

        public bool DateTimeValidate()
        {
            if (DataContratacaoInicio.HasValue && !DataContratacaoFim.HasValue)
                return false;
            if (!DataContratacaoInicio.HasValue && DataContratacaoFim.HasValue)
                return false;
            if (DataContratacaoInicio.HasValue && DataContratacaoFim.HasValue && DataContratacaoInicio.Value > DataContratacaoFim.Value)
                return false;

            return true;
        }

    }
}
