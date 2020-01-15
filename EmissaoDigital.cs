﻿namespace IntegracaoAPISandBox
{
    public class EmissaoDigital
    {
        public Sms Sms{ get; set; }
        public Email Email { get; set; }
    }

    public class Sms
    {
        public string Celular { get; set; }
    }

    public class Email
    {
        public string EnderecoDeEmail { get; set; }
        public string EnviarPorEmail { get; set; }
    }
}
