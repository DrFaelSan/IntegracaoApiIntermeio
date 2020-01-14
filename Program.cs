using IntegracaoAPISandBox.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IntegracaoAPISandBox
{

    public static class Program
    {
        static readonly string  BaseUrl = "https://sandbox.intermeiopagamentos.com";
        static readonly HttpClient client = new HttpClient();
        static Autenticacao Autenticacao;
        static BoletoUnitarioRetorno BoletoUnitario;
        static readonly Split Split = new Split();



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

                // Eviando boleto Unitário .... 
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{Autenticacao.AccessToken}");

                #region Mock Boleto exemplo
                Split.ClientesSplit.Add(new ClienteSplit
                {
                    NomeRazao = "Fabio Santos",
                    Documento = "97959424098",
                    Email = "teste@teste.com.br",
                    Celular = "11978556589",
                    Porcentagem = "16"
                });

                Split.ClientesSplit.Add(new ClienteSplit
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
                    Split,

                };
                #endregion


                response = await client.PostAsync(BaseUrl + "/Boleto", data1.AsJson());

                Console.WriteLine($"\nA resposta da requisição de Boleto Unitário foi .. : {response.StatusCode}");

                result = response.Content.ReadAsStringAsync().Result;

                if (result.Contains("Falha") || !response.IsSuccessStatusCode)
                    Console.WriteLine("\n-------------------------- Boleto Unitário ----------------------\n\n500 - Falha ao Emitir Boleto");
                
                else
                {
                    BoletoUnitario = JsonConvert.DeserializeObject<BoletoUnitarioRetorno>(result);

                    Console.WriteLine($"\n ------------------------ Boleto Unitário ------------------------ \n\n\n {BoletoUnitario} \n\n\n");

                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\n ------------------Erro -------------------\n\n Erro:" + e);
            }
        }

    }
}

