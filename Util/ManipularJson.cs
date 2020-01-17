using IntegracaoAPISandBox.Entidades;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace IntegracaoAPISandBox.Util
{
    public static class ManipularJson
    {
        public static StringContent AsJson(this object o)
             => new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");

        public static string Db = AppDomain.CurrentDomain.BaseDirectory + "BoletoLote1.json";
        public static BoletoUnitario Recuperar()
        {
            try { return JsonConvert.DeserializeObject<BoletoUnitario>(File.ReadAllText(Db)); }
            catch (Exception) { return default; }
        }
        public static void Salvar(BoletoUnitario Arquivo)
        {
            try { File.WriteAllText(Db, JsonConvert.SerializeObject(Arquivo)); }
            catch (JsonException ex) { throw ex; }
        }

    }
}
