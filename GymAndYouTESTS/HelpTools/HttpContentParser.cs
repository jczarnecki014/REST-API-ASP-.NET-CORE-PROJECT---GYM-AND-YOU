using Newtonsoft.Json;
using System.Text;

namespace GymAndYou.TESTS.HelpTools
{
    public static class HttpContentParser
    {
        public static HttpContent ToJsonHttpContent(this object obj)
        {   
            var objJson = JsonConvert.SerializeObject(obj);
            return new StringContent(objJson,Encoding.UTF8,"application/json");
        }
    }
}
