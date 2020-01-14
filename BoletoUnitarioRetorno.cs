namespace IntegracaoAPISandBox
{
    public class BoletoUnitarioRetorno
    {
        public string Status { get; set; }
        public BoletoRetorno Resultado { get; set; }

        public override string ToString() =>
                                $"Status: {Status}\n" +
                                $"Resultado: {Resultado}";
                                        
    }

    public class BoletoRetorno
    {
        public string NossoNumero { get; set; }
        public string HashBoleto { get; set; }
        public string LinhaDigitavel { get; set; }
        public string CodigoDeBarras { get; set; }
        public string NumeroDocumento { get; set; }
        public string Split { get; set; }
        //As propriedades citadas na documentação que estão abaixo não está maos nesse objeto de retorno.
        //Na documentação no arquivo Json de exemplo tem celular, porém no Objeto da Resquisação não cita nada sobre...
        public string DataPagamento { get; set; }
        public string ValorPago { get; set; }
        public string StatusBoleto { get; set; }
        public string Compensado { get; set; }
        public string DataCompensacao { get; set; }
        public string Processado { get; set; }


        /// <summary>
        /// A documentação atual está errada! lá está com esse Objeto porém só retorna: 
        ///     HashBoleto
        ///     LinhaDigitavel
        ///     CodigoDeBarras *OBS: na documentação está escrito: "CodigoDoCodigoDeBarras"
        ///     NossoNumero
        ///     NumeroDocumento
        ///     Split
        /// </summary>


        public override string ToString() =>
                $"\nNossoNumero {NossoNumero}\n" +
                $"Hash Boleto: {HashBoleto}\n" +
                $"Numero do Documento: {NumeroDocumento}\n" +
                $"Linha Digitavél: {LinhaDigitavel}\n" +
                $"Código do Código de Barras: {CodigoDeBarras}\n" +
                $"Split: {Split}";
    }
}
