using System;

namespace OnboardingSIGDB1.Domain.Filters
{
    public class EmpresaFilter
    {
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public DateTime? DataFundacaoInicio { get; set; }
        public DateTime? DataFundacaoFim { get; set; }

        public bool DateTimeValidate()
        {
            if (DataFundacaoInicio.HasValue && !DataFundacaoFim.HasValue)
                return false;
            if (!DataFundacaoInicio.HasValue && DataFundacaoFim.HasValue)
                return false;
            if (DataFundacaoInicio.HasValue && DataFundacaoFim.HasValue && DataFundacaoInicio.Value > DataFundacaoFim.Value)
                return false;

            return true;
        }

    }
}
