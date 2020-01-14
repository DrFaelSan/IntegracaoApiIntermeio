using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace IntegracaoAPISandBox.Util
{
    public static class ManipularJson
    {
        public static StringContent AsJson(this object o)
             => new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");
    }
}
