using IntegracaoAPISandBox.Util;
using Newtonsoft.Json;
using System;
using IntegracaoAPISandBox.Entidades;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Threading;

namespace IntegracaoAPISandBox
{
    public static class Metodos
    {
        public static string BaseUrl { get; set; } = "https://sandbox.intermeiopagamentos.com";

        public static async Task<string> PostLogin()
        {
            //Configurações para iniciar um requisição.
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Dados para enviar no body da requisição.
                var login = new Login { UserId = "bruno.f@inttecnologia.com.br", Password = "Senha123!" };

                //Configurações para enviar a requisição.
                HttpResponseMessage response = await client.PostAsync($"{BaseUrl}/Login", login.AsJson());
                string result = response.Content.ReadAsStringAsync().Result;
                var autenticacao = JsonConvert.DeserializeObject<Autenticacao>(result);

                Console.WriteLine($"\nA resposta da requisição de login foi .. : {response.StatusCode}");

                Console.WriteLine($"\n -----------------------Autenticação------------------------- \n\n\n{autenticacao} \n\n\n");

                return autenticacao.AccessToken;
            }
        }

        public static async void PostBoletoUnitario(string AccessToken)
        {
            //Configurações para iniciar um requisição.
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{AccessToken}");

                //Dados para enviar no body da requisição.

                #region Mock Boleto Unitario exemplo
                //Criando um split, instanciando uma lista de clientes e criando um cliente de forma simplificada.

                var split = new Split
                {
                    ClientesSplit = new List<ClienteSplit>
                {
                    new ClienteSplit
                    {
                        NomeRazao = "Fabio Santos",
                        Documento = "97959424098",
                        Email = "teste@teste.com.br",
                        Celular = "11978556589",
                        Porcentagem = "16"
                    }
                }
                };

                var boletoUnitario = new BoletoUnitario
                {
                    NomeSacado = "Rafael Teste HttpClient C#",
                    DocumentoSacado = "16004544027",
                    Logradouro = "AVENIDA",
                    EnderecoLogradouro = "Paulista",
                    ComplementoLogradouro = "Conjunto 10",
                    BairroLogradouro = "Jardim Paulista",
                    CepLogradouro = "04045004",
                    NumeroLogradouro = "1500",
                    CidadeLogradouro = "São Paulo",
                    EstadoLogradouro = "SP",
                    EmailSacado = "josedascouves@gmail.com",
                    TelefoneSacado = "11967654563",
                    NossoNumero = "3821738912",
                    NumeroDocumento = "38891592179213",
                    DataVencimento = "10/04/2020",
                    ValorTitulo = 15.11,
                    Instrucao1 = "",
                    Instrucao2 = "",
                    Instrucao3 = "",
                    Instrucao4 = "",
                    PercentualJuros = 0.1,
                    ValorJuros = 0,
                    QntDiasJuros = 3,
                    PercentualMulta = 0.1,
                    ValorMulta = 0,
                    QntDiasMultas = 3,
                    Split = split
                };



                #endregion

                //Configurações para enviar a requisição.
                HttpResponseMessage response = await client.PostAsync($"{BaseUrl}/Boleto", boletoUnitario.AsJson());

                ManipularJson.Salvar(boletoUnitario);

                string result = response.Content.ReadAsStringAsync().Result;

                Console.WriteLine($"\nA resposta da requisição de Boleto Unitário foi .. : {response.StatusCode}");

                if (result.Contains("Falha") || !response.IsSuccessStatusCode)
                    Console.WriteLine("\n-------------------------- Boleto Unitário ----------------------\n\n500 - Falha ao Emitir Boleto");

                else
                {
                    var BoletoUnitarioRes = JsonConvert.DeserializeObject<BoletoUnitarioRetorno>(result);

                    Console.WriteLine($"\n ------------------------ Boleto Unitário ------------------------ \n\n\n {BoletoUnitarioRes} \n\n\n");

                    response.EnsureSuccessStatusCode();
                }

            }
        }

        public static async void PostBoletoLote(string AccessToken)
        {
            //Configurações para iniciar um requisição.
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{AccessToken}");

                //Dados para enviar no body da requisição.

                #region Mock para Boleto Lote

                var emissaoDigital = new EmissaoDigital
                {
                    Email = new Email
                    {
                        EnderecoDeEmail = "josedascouves@gmail.com",
                        EnviarPorEmail = "true"
                    },
                    Sms = new Sms
                    {
                        Celular = "123456789"
                    }
                };

                //No exemplo utilizado não tem split no boleto lote.
                var split = new Split();

                var boletoUnitario = new BoletoParaLote
                {
                    NomeSacado = "Rafael Teste HttpClient C#",
                    DocumentoSacado = "16004544027",
                    Logradouro = "AVENIDA",
                    EnderecoLogradouro = "Paulista",
                    ComplementoLogradouro = "Conjunto 10",
                    BairroLogradouro = "Jardim Paulista",
                    CepLogradouro = "04045004",
                    NumeroLogradouro = "1500",
                    CidadeLogradouro = "São Paulo",
                    EstadoLogradouro = "SP",
                    EmailSacado = "josedascouves@gmail.com",
                    TelefoneSacado = "11967654563",
                    NumeroDocumento = "38891592179213",
                    DataVencimento = "10/04/2020",
                    ValorTitulo = "15.11",
                    Instrucao1 = "",
                    Instrucao2 = "",
                    Instrucao3 = "",
                    Instrucao4 = "",
                    PercentualJuros = "0.1",
                    ValorJuros = "0",
                    QntDiasJuros = "3",
                    PercentualMulta = "0.1",
                    ValorMulta = "0",
                    QntDiasMultas = "3",
                    Split = split,
                    EmissaoDigital = emissaoDigital
                };

                var boletoLote = new BoletoLote();
                boletoLote.ImpressaoEPostagem = "true";
                boletoLote.Boletos.Add(boletoUnitario);

                #endregion

                //Configurações para enviar a requisição.
                HttpResponseMessage response = await client.PostAsync($"{BaseUrl}/Boleto/Lote", boletoLote.AsJson());
                Thread.Sleep(1000);
                Console.WriteLine($"\nA resposta da requisição de Boleto Lote foi .. : {response.StatusCode}");

                string result = response.Content.ReadAsStringAsync().Result;

                var BoletoLoteResposta = JsonConvert.DeserializeObject<Retorno>(result);

                Console.WriteLine($"\n ----------------------- Boleto Lote ------------------------- \n\n\n{BoletoLoteResposta} \n\n\n");

                await Task.Delay(TimeSpan.FromSeconds(3));


            }
        }

        public static async void PostAlterarVencimento(string AccessToken)
        {
            //Configurações para iniciar um requisição.
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{AccessToken}");

                //Dados para enviar no body da requisição.
                var AlterarVencimento = new AlterarVencimento { HashBoleto = "bda74b64-ea15-480b-abea-b63e29015f02", DataVencimento = "15/02/2020" };

                //Configurações para enviar a requisição.
                HttpResponseMessage response = await client.PostAsync(BaseUrl + "/Lote/Vencimentos", AlterarVencimento.AsJson());
                Thread.Sleep(1000);
                string result = response.Content.ReadAsStringAsync().Result;
                var alteracaoVencimento = JsonConvert.DeserializeObject<Retorno>(result);

                Console.WriteLine($"\nA resposta da requisição de Alteração de vencimento foi .. : {response.StatusCode}");

                Console.WriteLine($"\n ----------------------- Alteração de vencimento ------------------------- \n\n\n{alteracaoVencimento} \n\n\n");

                await Task.Delay(TimeSpan.FromSeconds(3));
            }
        }

        public static async void PostBaixaTitulos(string AccessToken)
        {
            //Configurações para iniciar um requisição.
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{AccessToken}");

                //Dados para enviar no body da requisição.
                var BaixaTitulo = new AlterarVencimento { HashBoleto = "bda74b64-ea15-480b-abea-b63e29015f02" };

                //Configurações para enviar a requisição. 
                HttpResponseMessage response = await client.PostAsync(BaseUrl + "/Lote/Baixas", BaixaTitulo.AsJson());
                Thread.Sleep(1000);
                string result = response.Content.ReadAsStringAsync().Result;
                var BaixaTituloResp = JsonConvert.DeserializeObject<Retorno>(result);

                Console.WriteLine($"\nA resposta da requisição de Baixa de Títulos foi .. : {response.StatusCode}");

                Console.WriteLine($"\n ----------------------- Baixa de Títulos ------------------------- \n\n\n{BaixaTituloResp} \n\n\n");

                await Task.Delay(TimeSpan.FromSeconds(3));
            }
        }

        public static async void PostAbatimentoValores(string AccessToken)
        {
            //Configurações para iniciar um requisição.
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{AccessToken}");

                //Dados para enviar no body da requisição.
                var Abatimento = new AlterarVencimento { HashBoleto = "bda74b64-ea15-480b-abea-b63e29015f02", ValorAbatimento = "1000" };

                //Configurações para enviar a requisição. 
                HttpResponseMessage response = await client.PostAsync(BaseUrl + "/Lote/Abatimentos", Abatimento.AsJson());
                Thread.Sleep(1000);
                string result = response.Content.ReadAsStringAsync().Result;
                var AbatimentoResp = JsonConvert.DeserializeObject<Retorno>(result);

                Console.WriteLine($"\nA resposta da requisição de Abatimentos de Valores foi .. : {response.StatusCode}");

                Console.WriteLine($"\n ----------------------- Abatimentos de Valores ------------------------- \n\n\n{AbatimentoResp} \n\n\n");

                await Task.Delay(TimeSpan.FromSeconds(3));
            }
        }

        public static async void BuscandoBoletoUnitario(string AccessToken)
        {
            //Configurações para iniciar um requisição.
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{AccessToken}");

                //Configurações para enviar a requisição. 
                HttpResponseMessage response = await client.GetAsync(BaseUrl + "/Boleto/Consulta/c94c9383-ba7e-4d0f-bfa8-aa59e4bce1be");
                string result = response.Content.ReadAsStringAsync().Result;


                var RespostaBoleto = JsonConvert.DeserializeObject(result);

                Console.WriteLine($"\nA resposta da requisição de buscar Boleto Unitário foi .. : {response.StatusCode}");

                Console.WriteLine($"\n ----------------------- Boleto Unitário ------------------------- \n\n\n{RespostaBoleto} \n\n\n");


            }
        }

        public static async void BuscandoBoletoPorData(string AccessToken)
        {
            //Configurações para iniciar um requisição.
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{AccessToken}");

                //Configurações para enviar a requisição. 
                HttpResponseMessage response = await client.GetAsync(BaseUrl + "/Boleto/Consulta?dtInicio=2020-01-10&dtFim=2020-01-15&pag=1&qnt=25");
                //response = await client.GetAsync(BaseUrl + "/Boleto/Consulta?dtInicio=2020-01-10&dtFim=2020-01-15&status=3&pag=1&qnt=25");

                string result = response.Content.ReadAsStringAsync().Result;

                var RespostaBoletoData = JsonConvert.DeserializeObject(result);

                Console.WriteLine($"\nA resposta da requisição de buscar Boleto Unitário por Data foi .. : {response.StatusCode}");

                Console.WriteLine("\nAlterar Documentação página e quantidade de páginas Obrigatórios!\nInserir na Documentação o Status. ");

                //Console.WriteLine($"\n ----------------------- Boleto Unitário por Data ------------------------- \n\n\n{RespostaBoletoData} \n\n\n");
                Console.WriteLine($"\n ----------------------- Boleto Unitário por Data ------------------------- \n\n\n Descomente o Codigo Fonte para ver o resultado da Requisição \n\n\n");
            }

        }

        public static async void BuscandoBoletoEmLote(string AccessToken)
        {
            //Configurações para iniciar um requisição.
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{AccessToken}");

                //Configurações para enviar a requisição. 
                HttpResponseMessage response = await client.GetAsync(BaseUrl + "/Lote/7c566df5-cdea-4dd7-bc9c-41c4ae354ed5");

                string result = response.Content.ReadAsStringAsync().Result;

                var RespostaBoletoLote = JsonConvert.DeserializeObject(result);

                Console.WriteLine($"\nA resposta da requisição de buscar Boleto Em Lote foi .. : {response.StatusCode}");

                Console.WriteLine("Alterar endPoint na Documentação de Lote/Consulta/id para Lote/id");

                Console.WriteLine($"\n ----------------------- Boleto Unitário Em Lote ------------------------- \n\n\n{RespostaBoletoLote} \n\n\n");
            }

        }

        public static async void EnvioDeEmail(string AccessToken)
        {

            //Configurações para iniciar um requisição.
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{AccessToken}");

                //Dados para enviar no body da requisição.
                #region Mock Dados Email...

                var Destinatarios = new string[3];
                var Cc = new string[1];
                var Cco = new List<string>();
                Destinatarios[0] = "rafaelvplima1as@gmail.com";
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
                #endregion

                //Configurações para enviar a requisição. 
                HttpResponseMessage response = await client.PostAsync(BaseUrl + "/Email", dataEmail.AsJson());
                string result = response.Content.ReadAsStringAsync().Result;

                var EnvioEmail = JsonConvert.DeserializeObject(result);


                Console.WriteLine($"\nA resposta da requisição de Email foi .. : {response.StatusCode}");

                Console.WriteLine($"\n -----------------------Envio E-mail ------------------------- \n\n\n{EnvioEmail} \n\n\n");

            }

        }

        public static async void EnvioDeSms(string AccessToken)
        {
            //Configurações para iniciar um requisição.
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{AccessToken}");

                //Dados para enviar no body da requisição.
                #region Mock Dados Sms...
                var dataSms = new
                {
                    From = "Rafael",
                    Msg = "Olá esse é um sms de teste!",
                    Celular = "551198753130"
                };
                #endregion

                //Configurações para enviar a requisição. 
                HttpResponseMessage response = await client.PostAsync(BaseUrl + "/Sms", dataSms.AsJson());
                string result = response.Content.ReadAsStringAsync().Result;

                var EnvioDeSms = JsonConvert.DeserializeObject(result);

                Console.WriteLine($"\nA resposta da requisição de Sms foi .. : {response.StatusCode}");

                Console.WriteLine($"\n -----------------------Envio de Sms ------------------------- \n\n\n{EnvioDeSms} \n\n\n");

            }

        }
    }
}


