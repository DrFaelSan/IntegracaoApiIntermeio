namespace IntegracaoAPISandBox
{
    public class Boleto
    {
            public string NomeSacado { get; set; }
            public string  DocumentoSacado { get; set; }
            public string  HashBoleto { get; set; }
            public string  Logradouro { get; set; }
            public string  EnderecoLogradouro { get; set; }
            public string  ComplementoLogradouro { get; set; }
            public string  BairroLogradouro { get; set; }
            public string  CepLogradouro { get; set; }
            public string  NumeroLogradouro { get; set; }
            public string  CidadeLogradouro { get; set; }
            public string  EstadoLogradouro { get; set; }
            public string  EmailSacado { get; set; }
            public string  TelefoneSacado { get; set; }
            public string  NossoNumero { get; set; }
            public string  NumeroDocumento { get; set; }
            public string  DataVencimento { get; set; }
            public string  ValorTitulo { get; set; }
            public string  Instrucao1 { get; set; }
            public string  Instrucao2 { get; set; }
            public string  Instrucao3 { get; set; }
            public string  Instrucao4 { get; set; }
            public string  PercentualJuros { get; set; }
            public string  ValorJuros { get; set; }
            public string  QntDiasJuros { get; set; }
            public string  PercentualMulta { get; set; }
            public string  ValorMulta { get; set; }
            public string  QntDiasMultas { get; set; }
            public Split Split { get; set; }
            public EmissaoDigital EmissaoDigital { get; set; } 
    }

    public class BoletoParaLote
    {
        public string NomeSacado { get; set; }
        public string DocumentoSacado { get; set; }
        public string Logradouro { get; set; }
        public string EnderecoLogradouro { get; set; }
        public string ComplementoLogradouro { get; set; }
        public string BairroLogradouro { get; set; }
        public string CepLogradouro { get; set; }
        public string NumeroLogradouro { get; set; }
        public string CidadeLogradouro { get; set; }
        public string EstadoLogradouro { get; set; }
        public string EmailSacado { get; set; }
        public string TelefoneSacado { get; set; }
        public string NumeroDocumento { get; set; }
        public string DataVencimento { get; set; }
        public string ValorTitulo { get; set; }
        public string Instrucao1 { get; set; }
        public string Instrucao2 { get; set; }
        public string Instrucao3 { get; set; }
        public string Instrucao4 { get; set; }
        public string PercentualJuros { get; set; }
        public string ValorJuros { get; set; }
        public string QntDiasJuros { get; set; }
        public string PercentualMulta { get; set; }
        public string ValorMulta { get; set; }
        public string QntDiasMultas { get; set; }
        public Split Split { get; set; } = new Split();
        public EmissaoDigital EmissaoDigital { get; set; }
    }
}
