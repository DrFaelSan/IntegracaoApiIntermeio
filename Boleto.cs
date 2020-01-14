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
            public double  ValorTitulo { get; set; }
            public string  Instrucao1 { get; set; }
            public string  Instrucao2 { get; set; }
            public string  Instrucao3 { get; set; }
            public string  Instrucao4 { get; set; }
            public double  PercentualJuros { get; set; }
            public double  ValorJuros { get; set; }
            public int  QntDiasJuros { get; set; }
            public double  PercentualMulta { get; set; }
            public double  ValorMulta { get; set; }
            public int  QntDiasMultas { get; set; }
            public string Split { get; set; }


        // exemplo mock
        //nomeSacado = "Rafael Teste HttpClient C#",
        //documentoSacado = "16004544027",
        //hashBoleto = "2cd1c911-00b2-4c27-893f-b535e41a24a1",
        //logradouro = "AVENIDA",
        //enderecoLogradouro = "Paulista",
        //complementoLogradouro = "Conjunto 10",
        //bairroLogradouro = "Jardim Paulista",
        //cepLogradouro = "04045004",
        //numeroLogradouro = "1500",
        //cidadeLogradouro = "São Paulo",
        //estadoLogradouro = "SP",
        //emailSacado = "josedascouves@gmail.com",
        //telefoneSacado = "11967654563",
        //nossoNumero = "3821738912",
        //numeroDocumento = "38891592179213",
        //dataVencimento = "10/04/2020",
        //valorTitulo = 15.11,
        //instrucao1 = "",
        //instrucao2 = "",
        //instrucao3 = "",
        //instrucao4 = "",
        //percentualJuros = 0.1,
        //valorJuros = 0,
        //qntDiasJuros = 3,
        //percentualMulta = 0.1,
        //valorMulta = 0,
        //qntDiasMultas = 3,
        //Split,
    }
}
