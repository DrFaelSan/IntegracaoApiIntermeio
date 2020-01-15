using IntegracaoAPISandBox.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IntegracaoAPISandBox
{
    /// <summary>
    /// Programa Básico para integração com a API da intermeio no EndPoint de desenvolvimento.
    /// Basic program for integrating with Intemeio API in EndPoint Development.
    /// </summary>
    public static class Program
    {
        static readonly string BaseUrl = "https://sandbox.intermeiopagamentos.com";
        static readonly HttpClient client = new HttpClient();
        static Autenticacao Autenticacao;
        static BoletoUnitarioRetorno BoletoUnitario;
        static Split Split1 = new Split();
        static BoletoLote BoletoLote1;
        static BoletoParaLote Boleto1;
        static EmissaoDigital EmissaoDigital1;
        static Email Email1;
        static Sms Sms1;



        static void Main(string[] args)
        {

            Console.WriteLine("\nIniciando Resquisição SandBox Intermeio.\n");

            Resquisicoes().GetAwaiter().GetResult();

            Console.WriteLine("Pressione qualquer teclar para fechar o programa.\n");
            Console.ReadKey();

            Console.WriteLine("\n\n");

        }

        static async Task Resquisicoes()
        {
            client.BaseAddress = new Uri(BaseUrl);

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(

                new MediaTypeWithQualityHeaderValue("application/json")

                );

            try
            {
                // Fazendo Login...
                var data = new { UserId = "bruno.f@inttecnologia.com.br", Password = "Senha123!" };

                HttpResponseMessage response = await client.PostAsync(BaseUrl + "/Login", data.AsJson());

                string result = response.Content.ReadAsStringAsync().Result;

                Autenticacao = JsonConvert.DeserializeObject<Autenticacao>(result);


                Console.WriteLine($"\nA resposta da requisição de login foi .. : {response.StatusCode}");

                Console.WriteLine($"\n -----------------------Autenticação------------------------- \n\n\n{Autenticacao} \n\n\n");

                response.Dispose();

                // Enviando boleto Unitário .... 
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{Autenticacao.AccessToken}");

                #region Mock Boleto exemplo
                Split1.ClientesSplit = new List<ClienteSplit>();
                Split1.ClientesSplit.Add(new ClienteSplit
                {
                    NomeRazao = "Fabio Santos",
                    Documento = "97959424098",
                    Email = "teste@teste.com.br",
                    Celular = "11978556589",
                    Porcentagem = "16"
                });

                Split1.ClientesSplit.Add(new ClienteSplit
                {
                    NomeRazao = "Fulano da Silva",
                    Documento = "68445435060",
                    Email = "Fulano@teste.com.br",
                    Celular = "11988587848",
                    Porcentagem = "30"
                });

                var data1 = new
                {
                    nomeSacado = "Rafael Teste HttpClient C#",
                    documentoSacado = "16004544027",
                    logradouro = "AVENIDA",
                    enderecoLogradouro = "Paulista",
                    complementoLogradouro = "Conjunto 10",
                    bairroLogradouro = "Jardim Paulista",
                    cepLogradouro = "04045004",
                    numeroLogradouro = "1500",
                    cidadeLogradouro = "São Paulo",
                    estadoLogradouro = "SP",
                    emailSacado = "josedascouves@gmail.com",
                    telefoneSacado = "11967654563",
                    nossoNumero = "3821738912",
                    numeroDocumento = "38891592179213",
                    dataVencimento = "10/04/2020",
                    valorTitulo = 15.11,
                    instrucao1 = "",
                    instrucao2 = "",
                    instrucao3 = "",
                    instrucao4 = "",
                    percentualJuros = 0.1,
                    valorJuros = 0,
                    qntDiasJuros = 3,
                    percentualMulta = 0.1,
                    valorMulta = 0,
                    qntDiasMultas = 3,
                    Split1
                };
                #endregion


                response = await client.PostAsync(BaseUrl + "/Boleto", data1.AsJson());

                Console.WriteLine($"\nA resposta da requisição de Boleto Unitário foi .. : {response.StatusCode}");

                result = response.Content.ReadAsStringAsync().Result;

                //Caso de Falha em Enviar Boleto Unitário.
                if (result.Contains("Falha") || !response.IsSuccessStatusCode)
                    Console.WriteLine("\n-------------------------- Boleto Unitário ----------------------\n\n500 - Falha ao Emitir Boleto");

                else
                {
                    BoletoUnitario = JsonConvert.DeserializeObject<BoletoUnitarioRetorno>(result);

                    Console.WriteLine($"\n ------------------------ Boleto Unitário ------------------------ \n\n\n {BoletoUnitario} \n\n\n");

                    response.EnsureSuccessStatusCode();
                }

                //Enviando Boletos Lote...
                #region Mock para Boleto Lote

                Email1 = new Email
                {
                    EnderecoDeEmail = "josedascouves@gmail.com",
                    EnviarPorEmail = "true"
            };
            Sms1 = new Sms
            {
                Celular = "123456789"
            };
            EmissaoDigital1 = new EmissaoDigital
            {
                Email = Email1,
                Sms = Sms1
            };
                Split1 = new Split();
                Boleto1 = new BoletoParaLote
                {
                    NomeSacado = "José das Couves",
                    DocumentoSacado = "44572133816",
                    Logradouro = "Rua",
                    EnderecoLogradouro = "Onde os gatos perdeu as botas",
                    ComplementoLogradouro = "Casa",
                    BairroLogradouro = "Centro",
                    CepLogradouro = "04045004",
                    NumeroLogradouro = "10",
                    CidadeLogradouro = "Sao Paulo",
                    EstadoLogradouro = "SP",
                    EmailSacado = "josedascouves@gmail.com",
                    TelefoneSacado = "111111111",
                    NumeroDocumento = "33445566",
                    ValorTitulo = "999",
                    DataVencimento = "21/03/2019",
                    PercentualJuros = "10.0",
                    ValorJuros = "0",
                    QntDiasJuros = "3",
                    PercentualMulta = "10.0",
                    ValorMulta = "0",
                    QntDiasMultas = "3",
                    Instrucao1 = "Não receber após o vencimento",
                    Instrucao2 = "",
                    Instrucao3 = "",
                    Instrucao4 = "",
                    Split = Split1,
                    EmissaoDigital = EmissaoDigital1,
            };

            BoletoLote1 = new BoletoLote();
            BoletoLote1.ImpressaoEPostagem = "true";
            BoletoLote1.Boletos.Add(Boleto1);
            #endregion

            ManipularJson.Salvar(BoletoLote1);

            response = await client.PostAsync(BaseUrl + "/Boleto/Lote", BoletoLote1.AsJson());

            Console.WriteLine($"\nA resposta da requisição de Boleto Lote foi .. : {response.StatusCode}");

            result = response.Content.ReadAsStringAsync().Result;

            var BoletoLoteResposta = JsonConvert.DeserializeObject(result);

            Console.WriteLine($"\n ----------------------- Boleto Lote ------------------------- \n\n\n{BoletoLoteResposta} \n\n\n");

                //Enviando Requisição Alteração de vencimento

                var data2 = new { hashBoleto = "bda74b64-ea15-480b-abea-b63e29015f02", dataVencimento = "15/02/2020" };

                 response = await client.PostAsync(BaseUrl + "/Lote/Vencimentos", data2.AsJson());

                result = response.Content.ReadAsStringAsync().Result;

                var alteracaoVencimento = JsonConvert.DeserializeObject(result);


                Console.WriteLine($"\nA resposta da requisição de Alteração de vencimento foi .. : {response.StatusCode}");

                Console.WriteLine($"\n ----------------------- Alteração de vencimento ------------------------- \n\n\n{alteracaoVencimento} \n\n\n");


                //Enviando Requisição Baixa de Títulos

                var data3 = new { hashBoleto = "bda74b64-ea15-480b-abea-b63e29015f02" };

                 response = await client.PostAsync(BaseUrl + "/Lote/Baixas", data3.AsJson());

                result = response.Content.ReadAsStringAsync().Result;

                var BaixaTitulos = JsonConvert.DeserializeObject(result);


                Console.WriteLine($"\nA resposta da requisição de Baixa de Títulos foi .. : {response.StatusCode}");

                Console.WriteLine($"\n ----------------------- Baixa de Títulos ------------------------- \n\n\n{BaixaTitulos} \n\n\n");

                //Enviando Requisição Abatimentos de Valores

                var data4 = new { hashBoleto = "bda74b64-ea15-480b-abea-b63e29015f02" , valorAbatimento = "1000"};

                 response = await client.PostAsync(BaseUrl + "/Lote/Abatimentos", data4.AsJson());

                result = response.Content.ReadAsStringAsync().Result;

                var Abatimento = JsonConvert.DeserializeObject(result);


                Console.WriteLine($"\nA resposta da requisição de Abatimentos de Valores foi .. : {response.StatusCode}");

                Console.WriteLine($"\n ----------------------- Abatimentos de Valores ------------------------- \n\n\n{Abatimento} \n\n\n");

                //Buscando Boleto Unitário

                response = await client.GetAsync(BaseUrl + "/Boleto/Consulta/c94c9383-ba7e-4d0f-bfa8-aa59e4bce1be");

                result = response.Content.ReadAsStringAsync().Result;

                var RespostaBoleto = JsonConvert.DeserializeObject(result);

                Console.WriteLine($"\nA resposta da requisição de buscar Boleto Unitário foi .. : {response.StatusCode}");

                Console.WriteLine($"\n ----------------------- Boleto Unitário ------------------------- \n\n\n{RespostaBoleto} \n\n\n");


                //Buscando Boleto Unitário por Data

                response = await client.GetAsync(BaseUrl + "/Boleto/Consulta?dtInicio=2020-01-10&dtFim=2020-01-15&pag=1&qnt=25");
                //response = await client.GetAsync(BaseUrl + "/Boleto/Consulta?dtInicio=2020-01-10&dtFim=2020-01-15&status=3&pag=1&qnt=25");

                result = response.Content.ReadAsStringAsync().Result;

                var RespostaBoletoData = JsonConvert.DeserializeObject(result);

                Console.WriteLine($"\nA resposta da requisição de buscar Boleto Unitário por Data foi .. : {response.StatusCode}");
                
                Console.WriteLine("\nAlterar Documentação página e quantidade de páginas Obrigatórios!\nInserir na Documentação o Status. ");

                //Console.WriteLine($"\n ----------------------- Boleto Unitário por Data ------------------------- \n\n\n{RespostaBoletoData} \n\n\n");
                Console.WriteLine($"\n ----------------------- Boleto Unitário por Data ------------------------- \n\n\n Descomente o Codigo Fonte para ver o resultado da Requisição \n\n\n");



                //Buscando Boleto Em Lote

                response = await client.GetAsync(BaseUrl + "/Lote/7c566df5-cdea-4dd7-bc9c-41c4ae354ed5");

                result = response.Content.ReadAsStringAsync().Result;

                var RespostaBoletoLote = JsonConvert.DeserializeObject(result);

                Console.WriteLine($"\nA resposta da requisição de buscar Boleto Em Lote foi .. : {response.StatusCode}");

                Console.WriteLine("Alterar endPoint na Documentação de Lote/Consulta/id para Lote/id");

                Console.WriteLine($"\n ----------------------- Boleto Unitário Em Lote ------------------------- \n\n\n{RespostaBoletoLote} \n\n\n");

                /// Testando Envio de E-mail e SMS.

                var Destinatarios = new string[3];
            var Cc = new string[1];
            var Cco = new List<string>();
            Destinatarios[0] = "rafaelvplima@gmail.com";
            Destinatarios[1] = "geancarloss123@outlook.com";
            Destinatarios[2] = "geancarloss123@outlook.com";
            Cc[0] = "matheus-397@hotmail.com ";
            var Anexo = new List<object>();
            Anexo.Add(new
            {
                NomeArquivo = "teste.pdf",
                BytesArquivos = "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9saccGrSqvRvqiuXI5FrrFmq1JpO/nH0eyef5qowWaeQFPmBOtjmHg/LrCpxnmeVpXbWVVVetvy1ooe7fznf7/0C2uG+Yw0KZW5kc3RyZWFtDQplbmRvYmoNCjE5IDAgb2JqDQpbIDI3OCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgNzIyIDAgMCAwIDAgMjc4IDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgNTAwIDU1NiA1NTYgMCAwIDAgMjIyIDAgMCAwIDgzMyA1NTYgNTU2IDAgMCAzMzMgNTAwIDI3OCA1NTZdIA0KZW5kb2JqDQoyMCAwIG9iag0KPDwvVHlwZS9YUmVmL1NpemUgMjAvV1sgMSA0IDJdIC9Sb290IDEgMCBSL0luZm8gOSAwIFIvSURbPDdBODZCODNBMDUzOUY1NEM5RDAwQ0Q5RjE2QzdDRDdEPjw3QTg2QjgzQTA1MzlGNTRDOUQwMENEOUYxNkM3Q0Q3RD5dIC9GaWx0ZXIvRmxhdGVEZWNvZGUvTGVuZ3RoIDgwPj4NCnN0cmVhbQ0KeJxjYACC//8ZgaQgAwOIqoVQW8EU42EwxfQHTDHPBlMs1RBqA4R6CpQHahBgYIFQrBCKDUIxQyioEnagBtbTMB4jhGICCrInMzAAAGziCa8NCmVuZHN0cmVhbQ0KZW5kb2JqDQp4cmVmDQowIDIxDQowMDAwMDAwMDEwIDY1NTM1IGYNCjAwMDAwMDAwMTcgMDAwMDAgbg0KMDAwMDAwMDEyNSAwMDAwMCBuDQowMDAwMDAwMTgxIDAwMDAwIG4NCjAwMDAwMDA0NTEgMDAwMDAgbg0KMDAwMDAwMDc2NCAwMDAwMCBuDQowMDAwMDAwOTIzIDAwMDAwIG4NCjAwMDAwMDExNDcgMDAwMDAgbg0KMDAwMDAwMTIwMCAwMDAwMCBuDQowMDAwMDAxMjUzIDAwMDAwIG4NCjAwMDAwMDAwMTEgNjU1MzUgZg0KMDAwMDAwMDAxMiA2NTUzNSBmDQowMDAwMDAwMDEzIDY1NTM1IGYNCjAwMDAwMDAwMTQgNjU1MzUgZg0KMDAwMDAwMDAxNSA2NTUzNSBmDQowMDAwMDAwMDE2IDY1NTM1IGYNCjAwMDAwMDAwMTcgNjU1MzUgZg0KMDAwMDAwMDAxOCA2NTUzNSBmDQowMDAwMDAwMDAwIDY1NTM1IGYNCjAwMDAwMDE4OTEgMDAwMDAgbg0KMDAwMDAwMjExNCAwMDAwMCBuDQp0cmFpbGVyDQo8PC9TaXplIDIxL1Jvb3QgMSAwIFIvSW5mbyA5IDAgUi9JRFs8N0E4NkI4M0EwNTM5RjU0QzlEMDBDRDlGMTZDN0NEN0Q+PDdBODZCODNBMDUzOUY1NEM5RDAwQ0Q5RjE2QzdDRDdEPl0gPj4NCnN0YXJ0eHJlZg0KMjM5Mw0KJSVFT0YNCnhyZWYNCjAgMA0KdHJhaWxlcg0KPDwvU2l6ZSAyMS9Sb290IDEgMCBSL0luZm8gOSAwIFIvSURbPDdBODZCODNBMDUzOUY1NEM5RDAwQ0Q5RjE2QzdDRDdEPjw3QTg2QjgzQTA1MzlGNTRDOUQwMENEOUYxNkM3Q0Q3RD5dIC9QcmV2IDIzOTMvWFJlZlN0bSAyMTE0Pj4NCnN0YXJ0eHJlZg0KMjk2OA0KJSVFT0Y="
            });
            var Assunto = "Teste Email";
            var Msg = "Mensagem de Teste!";

            var dataEmail = new
            {
                Destinatarios,
                Cc,
                Cco,
                Anexo,
                Assunto,
                Msg
            };

            response = await client.PostAsync(BaseUrl + "/Email", dataEmail.AsJson());

            result = response.Content.ReadAsStringAsync().Result;

            var EnvioEmail = JsonConvert.DeserializeObject(result);


            Console.WriteLine($"\nA resposta da requisição de Email foi .. : {response.StatusCode}");

            Console.WriteLine($"\n -----------------------Envio E-mail ------------------------- \n\n\n{EnvioEmail} \n\n\n");

            var dataSms = new
            {
                From = "Rafael",
                Msg = "Olá esse é um sms de teste!",
                Celular = "551198753130"
            };

            response = await client.PostAsync(BaseUrl + "/Sms", dataSms.AsJson());

            result = response.Content.ReadAsStringAsync().Result;

            var EnvioDeSms = JsonConvert.DeserializeObject(result);

            Console.WriteLine($"\nA resposta da requisição de Sms foi .. : {response.StatusCode}");

            Console.WriteLine($"\n -----------------------Envio de Sms ------------------------- \n\n\n{EnvioDeSms} \n\n\n");


        }   
            catch (Exception e)
            {
                Console.WriteLine("\n\n ------------------Erro -------------------\n\n Erro:" + e);
            }
        }

    }
}

