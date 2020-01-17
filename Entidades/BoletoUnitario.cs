using System.Collections.Generic;
namespace IntegracaoAPISandBox.Entidades
{
    public class BoletoUnitario
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
        public string NossoNumero { get; set; }
        public string NumeroDocumento { get; set; }
        public string DataVencimento { get; set; }
        public double ValorTitulo { get; set; }
        public string Instrucao1 { get; set; }
        public string Instrucao2 { get; set; }
        public string Instrucao3 { get; set; }
        public string Instrucao4 { get; set; }
        public double PercentualJuros { get; set; }
        public double ValorJuros { get; set; }
        public int QntDiasJuros { get; set; }
        public double PercentualMulta { get; set; }
        public double ValorMulta { get; set; }
        public int QntDiasMultas { get; set; }
        public Split Split { get; set; }
        //public string HashBoleto { get; set; }
        public EmissaoDigital EmissaoDigital { get; set; }
        //Atributos abaixo não são obrigatórios na requisião de POST/Boleto Unitário. 
        //public string ControleBoletoCliente { get; set; }
        //public string ValorPago { get; set; }
        //public string JurosPago { get; set; }
        //public string DataPagamento { get; set; }
        //public string MultaPaga { get; set; }
        //public string StatusBoleto { get; set; }
        //public string DataDaBaixa { get; set; }
        //public string ConfirmacaoCliente { get; set; }
        //public string ContaEmissao { get; set; }
        //public string LinhaDigitavel { get; set; }
        //public string Celular { get; set; }
        //public string BloqueadoAte { get; set; }
        //public string Compensado { get; set; }
        //public string DataCompensacao { get; set; }
        //public string Processado { get; set; }
        //public string SmsEnviados { get; set; }
        //public string EmailsEnviados { get; set; }
        //public string LoteId { get; set; }
        //public string DataEnvioGrafica { get; set; }
        //public string ImpressaoEPostado { get; set; }
        //public string ImprimirEPostar { get; set; }
        //public string LayoutImpressao { get; set; }
        //public string LayoutImpressaoId { get; set; }
        //public string DataCadastro { get; set; }
    }
    //Objeto Split criado para enviar no boleto unitário.
    public class Split
    {
        public List<ClienteSplit> ClientesSplit { get; set; }

        /// <summary>
        ///  A documentação está exibindo o retorno errado... 
        ///  ao invés de mostrar a lista de ClientesSplit está mostrando apenas true ou false.
        /// </summary>
        public override string ToString()
        {
            if (ClientesSplit.Count > 0)
            {
                foreach (var Cliente in ClientesSplit)
                    return $"Cliente Split: {Cliente}";
            }
            return $"Split : false";
        }
    }

    public class ClienteSplit
    {
        public string NomeRazao { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Porcentagem { get; set; }

        public override string ToString() => $"Nome ou Razão Split: {NomeRazao}\n" +
                    $"Documento Split: {Documento}\n" +
                    $"Email Split: {Email}\n" +
                    $"Celular Split: {Celular}\n" +
                    $"Porcentagem Split: {Porcentagem}\n";
        
    }

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
