namespace IntegracaoAPISandBox.Entidades
{
    
    public class Retorno
    {
        public string Status { get; set; }
        public dynamic Resultado { get; set; }

        public override string ToString() => $"Status: {Status}\nResultado: {Resultado}";
    }
}
