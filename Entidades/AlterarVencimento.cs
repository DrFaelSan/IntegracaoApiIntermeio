using System;
using System.Collections.Generic;
using System.Text;

namespace IntegracaoAPISandBox.Entidades
{
    public class AlterarVencimento
    {
        public string HashBoleto { get; set; }
        public string DataVencimento { get; set; }
        public string ValorAbatimento { get; set; }
    }
}
