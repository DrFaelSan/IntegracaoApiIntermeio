using System.Collections.Generic;


namespace IntegracaoAPISandBox
{
    public class Split
    {
        public List<ClienteSplit> ClientesSplit { get; set; } = new List<ClienteSplit>();

        /// <summary>
        ///  A documentação está exibindo o retorno errado... 
        ///  ao invés de mostrar a lista de ClientesSplit está mostrando apenas true ou false.
        /// </summary>
        /// 
        public override string ToString()
        {
            if (ClientesSplit.Count > 0)
            {
                foreach (var Cliente in ClientesSplit)
                    return $"Cliente Split: {Cliente}";
            }
            return $"Split vazio";
        }
    }

    public class ClienteSplit
    {
        public string NomeRazao { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Porcentagem { get; set; }

        public override string ToString()
        {
            return $"Nome ou Razão Split: {NomeRazao}\n" +
                    $"Documento Split: {Documento}\n" +
                    $"Email Split: {Email}\n" +
                    $"Celular Split: {Celular}\n" +
                    $"Porcentagem Split: {Porcentagem}\n";
        }
    }
}
