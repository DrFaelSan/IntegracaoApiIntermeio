using System;
using System.Collections.Generic;
using System.Text;

namespace IntegracaoAPISandBox
{
    public class BoletoLote
    {
        public string ImpressaoEPostagem { get; set; }
        public List<BoletoParaLote> Boletos { get; set; } = new List<BoletoParaLote>();

    }
}
