﻿namespace IntegracaoAPISandBox.Entidades
{
    //Objeto para envido de dados na requisição de Login.
    public  class Login
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }

    //Objeto Autenticacao feito para mapeamendo do retorno do login.
    public class Autenticacao
    {
        public bool Authenticated { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }

        public override string ToString() => $"Autenticado: {Authenticated} \n" +
                                             $"Criado: {Created}\n" +
                                             $"Expiração: {Expiration}\n" +
                                             $"Bearer JWT/ Token de Acesso: {AccessToken.Substring(0, 20)}... \n" +
                                             $"Mensagem: {Message}";

    }
}
