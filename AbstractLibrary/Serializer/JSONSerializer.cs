using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractLibrary.Serializer
{
    public class JSONSerializer : ISerializer<String>
    {

        public object Deserialize(string data)
        {
            return JsonConvert.DeserializeObject(data);
        }

        public string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
